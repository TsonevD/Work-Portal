using System.Collections.Generic;
using WorkPortal.Data;
using WorkPortal.Services.AnnualLeaves.Models;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using WorkPortal.Models.AnnualLeave;
using Models;
using Models.Enums;

namespace WorkPortal.Services.AnnualLeaves
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

        public ICollection<AllAnnualLeavesServiceModel> All()
        {
            var all = this.data.AnnualLeaves
                .Where(x => x.Status == AnnualLeaveStatus.Pending)
                .ProjectTo<AllAnnualLeavesServiceModel>(mapper)
                .ToList();

            return all;
        }

        public ICollection<AnnualLeaveServiceModel> Mine(int userId)
        {
            var mine = this.data.AnnualLeaves
                .Where(x => x.EmployeeId == userId)
                .ProjectTo<AnnualLeaveServiceModel>(mapper)
                .ToList();

            return mine;
        }



        public int Add(AnnualLeaveInputModel annualLeave , string userById)
        {
            var employee = this.data.Employees.First(x => x.UserId == userById);

            var newLeaveRequest = new AnnualLeave()
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

        public void Approve(int id)
        {
            var annualLeave = this.data.AnnualLeaves.Find(id);

            annualLeave.Status = AnnualLeaveStatus.Approved;
            
            this.data.SaveChanges();
        }

        public void Decline(int id)
        {
            var annualLeave = this.data.AnnualLeaves.Find(id);

            annualLeave.Status = AnnualLeaveStatus.Declined;

            this.data.SaveChanges();
        }

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

    }
}
