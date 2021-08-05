using WorkPortal.Data;
using WorkPortal.Services.AnnualLeave.Models;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using WorkPortal.Models.AnnualLeave;

namespace WorkPortal.Services.AnnualLeave
{
    public class AnnualLeaveService : IAnnualLeaveService
    {
        private readonly WorkPortalDbContext data;
        private readonly IConfigurationProvider mapper;

        public AnnualLeaveService(WorkPortalDbContext data, IConfigurationProvider mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }


        public AllAnnualLeavesServiceModel All(string userId)
        {
            var all = this.data.Employees
                .Where(x => x.UserId == userId)
                .Select(x => new AllAnnualLeavesServiceModel()
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    AnnualLeave = x.AnnualLeaves.Select(a => new AnnualLeaveServiceModel()
                    {
                        Id = a.Id,
                        DaysToBeTaken = (int)a.DaysToBeTaken,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        Status = a.Status,
                        Reason = a.Reason,
                        LeaveType = a.Type,
                    }).ToList(),
                })
                .FirstOrDefault();

            return all;
        }

        public int Add(AnnualLeaveInputModel annualLeave , string userById)
        {
            var employee = this.data.Employees.First(x => x.UserId == userById);

            var newLeaveRequest = new global::Models.AnnualLeave()
            {
                EmployeeId = employee.Id,
                StartDate = annualLeave.StartDate,
                EndDate = annualLeave.EndDate,
                Reason = annualLeave.Reason,
                Type = annualLeave.LeaveType,
                DaysToBeTaken = annualLeave.DaysToBeTaken,
            };
            this.data.AnnualLeaves.Add(newLeaveRequest);
            employee.AnnualLeaves.Add(newLeaveRequest);

            this.data.SaveChanges();

            return newLeaveRequest.Id;
        }

        public AnnualLeaveDetailsServiceModel EditDetails(int id, int userId) 
            => this.data.AnnualLeaves
                .Where(x => x.Id == id && x.EmployeeId == userId)
                .ProjectTo<AnnualLeaveDetailsServiceModel>(this.mapper)
                .FirstOrDefault();

        public void Edit(int id , AnnualLeaveInputModel annualLeaveEdit, int userId)
        {
            var annualLeave = this.data.AnnualLeaves.Find(id);

            annualLeave.StartDate = annualLeaveEdit.StartDate;
            annualLeave.EndDate = annualLeaveEdit.EndDate;
            annualLeave.Type = annualLeaveEdit.LeaveType;
            annualLeave.Reason = annualLeaveEdit.Reason;
            annualLeave.DaysToBeTaken = annualLeaveEdit.DaysToBeTaken;

            this.data.SaveChanges();
        }

        public void Delete(int id)
        {
            var annualLeave = this.data.AnnualLeaves.Find(id);

            this.data.AnnualLeaves.Remove(annualLeave);

            this.data.SaveChanges();
        }

        public bool IsByUser(int id, int userId)
            => this.data.AnnualLeaves
                .Any(x => x.Id == id && x.EmployeeId == userId);

        public int UserId(string userId)
            => this.data
                .Employees
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

    }
}
