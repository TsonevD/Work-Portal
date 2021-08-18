using System.Collections.Generic;
using WorkPortal.Services.AnnualLeaves.Models;
using WorkPortal.Services.Payslips.Models;
using WorkPortal.Services.Shifts.Models;

namespace WorkPortal.Services.Payslips
{
    public interface IPayslipService
    {

        ICollection<EmployeeServiceModel> AllEmployees();

        ICollection<AnnualLeaveServiceModel> EmployeeAnnualLeave(int id , int monthId);

        ICollection<ShiftQueryModel> EmployeeShifts(int id , int monthId);

        ICollection<PayslipServiceModel> EmployeePayslips(int id);

        (decimal, decimal) GetAnnualLeaveData(int id, int monthId);

        (decimal, decimal) GetShiftData(int id, int monthId);

        void Add(int employeeId ,int monthId, decimal leaveHours , decimal leaveMoney , decimal shiftsHours, decimal shiftMoney);

    }
}
