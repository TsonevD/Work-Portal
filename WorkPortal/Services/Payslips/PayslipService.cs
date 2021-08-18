using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Models;
using Models.Enums;
using WorkPortal.Data;
using WorkPortal.Services.AnnualLeaves.Models;
using WorkPortal.Services.Payslips.Models;
using WorkPortal.Services.Shifts;
using WorkPortal.Services.Shifts.Models;

namespace WorkPortal.Services.Payslips
{
    public class PayslipService : IPayslipService
    {
        private const decimal HOURS_WORKING = 8;
        private const decimal ANNUAL_LEAVE_RATE = 13;
        private const decimal PAYSLIP_TAX = 0.80m;

        private readonly WorkPortalDbContext data;
        private readonly IConfigurationProvider mapper;
        private readonly IShiftService shiftService;

        public PayslipService(WorkPortalDbContext data, IConfigurationProvider mapper
            , IShiftService shiftService)
        {
            this.data = data;
            this.mapper = mapper;
            this.shiftService = shiftService;
        }

        public ICollection<EmployeeServiceModel> AllEmployees()
        {
            var all = shiftService
                .AllEmployees();

            return all;
        }

        public ICollection<AnnualLeaveServiceModel> EmployeeAnnualLeave(int id, int monthId)
        {
            var leaves = this.data.AnnualLeaves
                .Where(x => x.EmployeeId == id && x.Type != AnnualLeaveType.UnpaidLeave
                            && x.EndDate.Month == monthId
                    && x.Type != AnnualLeaveType.UnpaidLeave)
                .ProjectTo<AnnualLeaveServiceModel>(mapper)
                .ToList();

            return leaves;
        }

        public ICollection<ShiftQueryModel> EmployeeShifts(int id, int monthId)
        {
            var shifts = this.data.Shifts
                .Where(x => x.EmployeeId == id
                        && x.ShiftDate.Month == monthId)
                .ProjectTo<ShiftQueryModel>(mapper)
                .ToList();

            return shifts;
        }

        public ICollection<PayslipServiceModel> EmployeePayslips(int id)
        {
            var payslips = this.data.Payslips
                .Where(x => x.EmployeeId == id)
                .ProjectTo<PayslipServiceModel>(mapper)
                .ToList();

            return payslips;
        }

        public (decimal, decimal) GetAnnualLeaveData(int id, int monthId)
        {
            var allAnnualLeaves = this.data.AnnualLeaves
                .Where(x => x.EmployeeId == id && x.Type != AnnualLeaveType.UnpaidLeave
                            && x.EndDate.Month == monthId)
                .ToList();

            var totalHours = 0m;
            var totalMoney = 0m;
            if (!allAnnualLeaves.Any()) return (totalMoney, totalHours);

            foreach (var leave in allAnnualLeaves)
            {
                var leaveHours = (leave.DaysToBeTaken * HOURS_WORKING);
                var rate = leaveHours * ANNUAL_LEAVE_RATE;

                totalHours += (decimal)leaveHours;
                totalMoney += (decimal)rate;
            }

            return (totalMoney, totalHours);
        }

        public (decimal, decimal) GetShiftData(int id, int monthId)
        {
            var calc = this.data.Shifts
                .Where(x => x.EmployeeId == id
                            && x.ShiftDate.Month == monthId)
                .ToList();

            var totalHours = 0m;
            var totalMoney = 0m;
            foreach (var shift in calc)
            {
                var dayRate = shift.HoursWorking * shift.RatePerHour;
                totalHours += shift.HoursWorking;
                totalMoney += dayRate;
            }

            return (totalMoney, totalHours);
        }

        public void Add(int employeeId, int monthId, decimal leaveHours, decimal leaveMoney, decimal shiftsHours, decimal shiftMoney)
        {
            var beforeTax = leaveMoney + shiftMoney;
            var afterTax = beforeTax * PAYSLIP_TAX;
            var totalHours = leaveHours + shiftsHours;

            var payslip = new Payslip()
            {
                EmployeeId = employeeId,
                Month = monthId,
                Year = DateTime.UtcNow.Year,
                BeforeTaxSalary = beforeTax,
                AfterTaxSalary = afterTax,
                Taxes = beforeTax - afterTax,
                WorkingHoursPerMonth = totalHours,
            };

            this.data.Payslips.Add(payslip);
            this.data.SaveChanges();
        }
    }
}
