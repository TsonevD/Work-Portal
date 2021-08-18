using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using WorkPortal.Infrastructure;
using WorkPortal.Services.Employees;
using WorkPortal.Services.Payslips;

namespace WorkPortal.Controllers
{
    public class PayslipController : Controller
    {
        private readonly IPayslipService payslipService;
        private readonly IEmployeeService employeeService;

        public PayslipController(IPayslipService payslipService, IEmployeeService employeeService)
        {
            this.payslipService = payslipService;
            this.employeeService = employeeService;
        }


        [Authorize]
        public IActionResult Mine()
        {
            var id = this.User.GetId();
            var userId = employeeService.UserId(id);

            var payslips = payslipService.EmployeePayslips(userId);

            return View(payslips);
        }
    }
}
