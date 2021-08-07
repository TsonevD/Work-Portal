using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Models;
using WorkPortal.Data;
using WorkPortal.Services.Shifts.Models;

namespace WorkPortal.Services.Shifts
{
    public class ShiftService : IShiftService
    {
        private readonly WorkPortalDbContext data;
        private readonly IConfigurationProvider mapper;

        public ShiftService(WorkPortalDbContext data, IConfigurationProvider mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public void Add(ShiftInputServiceModel shift)
        {
            var currentShift = new Shift()
            {
                ShiftDate = shift.ShiftDate,
                StartTime = shift.StartTime,
                FinishTime = shift.FinishTime,
                HoursWorking = shift.HoursWorking,
                RatePerHour = shift.RatePerHour,
                Location = new Location()
                {
                    CompanyName = shift.Location.CompanyName,
                    PostCode = shift.Location.PostCode,
                    StreetName = shift.Location.StreetName,
                    StreetNumber = shift.Location.StreetNumber,
                    Town = shift.Location.Town,
                }
            };

            this.data.Shifts.Add(currentShift);
            this.data.SaveChanges();
        }
        public ICollection<ShiftQueryModel> All()
        {
            var allShifts = this.data
                .Shifts
                .Where(x => x.IsAssigned == false)
                .ProjectTo<ShiftQueryModel>(mapper)
                .ToList();

            return allShifts;
        }

        public void Assign(int id , int employeeId)
        {
            var shift = this.data
                .Shifts
                .FirstOrDefault(x => x.Id == id);

            shift.IsAssigned = true;
            shift.EmployeeId = employeeId;

            this.data.SaveChanges();
        }



        public ICollection<EmployeeServiceModel> AllEmployees()
        {
            var all = this.data
                .Employees
                .ProjectTo<EmployeeServiceModel>(mapper)
                .ToList();

            return all;
        }

        public ICollection<ShiftQueryModel> Mine(int id)
        {
            var shifts = this.data
                .Shifts
                .Where(x => x.EmployeeId == id)
                .ProjectTo<ShiftQueryModel>(mapper)
                .ToList();

            return shifts;
        }

        public ShiftQueryModel Details(int id)
        {
            var shift = this.data
                .Shifts
                .Where(x => x.Id == id)
                .ProjectTo<ShiftQueryModel>(mapper)
                .FirstOrDefault();

            return shift;
        }

        public ShiftQueryModel FindShift(int id)
        {
            var shift = this.data
                .Shifts
                .Where(x => x.Id == id)
                .ProjectTo<ShiftQueryModel>(mapper)
                .FirstOrDefault();

            return shift;
        }
    }
}
