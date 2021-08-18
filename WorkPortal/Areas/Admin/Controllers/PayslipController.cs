using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPortal.Infrastructure;
using WorkPortal.Services.Payslips;
using WorkPortal.Services.Payslips.Models;

namespace WorkPortal.Areas.Admin.Controllers
{
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class PayslipController : Controller
    {
        private readonly IPayslipService payslipService;

        public PayslipController(IPayslipService payslipService)
            => this.payslipService = payslipService;

        public IActionResult Employees()
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }
            var all = payslipService
                .AllEmployees();

            return View(new PayslipAssignModel()
            {
                Employees = all,
            });
        }

        public IActionResult All(int employeeId, int monthId)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }
            var allShifts = payslipService
                .EmployeeShifts(employeeId, monthId);
            var allAnnualLeave = payslipService
                .EmployeeAnnualLeave(employeeId, monthId);

            return View(new PayslipQueryModel()
            {
                EmployeeId = employeeId,
                MonthId = monthId,
                Shifts = allShifts,
                AnnualLeave = allAnnualLeave
            });
        }

        public IActionResult Generate(int employeeId, int monthId)
        {
            ;
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }

            var (leaveMoney, leaveHours) = payslipService
                .GetAnnualLeaveData(employeeId, monthId);
            var (shiftsMoney, shiftsHours) = payslipService
                .GetShiftData(employeeId, monthId);

            payslipService.Add(employeeId, monthId ,leaveHours, leaveMoney, shiftsHours, shiftsMoney);

            return RedirectToAction(nameof(Employees));
        }
    }
}
