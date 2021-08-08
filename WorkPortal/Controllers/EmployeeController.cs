using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPortal.Infrastructure;
using WorkPortal.Services.Employees;
using WorkPortal.Services.Employees.Models;

namespace WorkPortal.Controllers
{
    public class EmployeeController : Controller 
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [Authorize]
        public IActionResult Profile()
        {
            var userId = this.User.GetId();

            var profile = employeeService.GetProfile(userId);

            return View(profile);
        }

        [Authorize]
        public IActionResult CompleteProfile()
        {
            var departments = employeeService.GetDepartments();

            var managers = employeeService.GetManagers();

            var view = new ProfileServiceModel()
            {
                Departments = departments,
                Managers = managers,
            };

            return View(view);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CompleteProfile(ProfileServiceModel profile)
        {
            var userId = this.User.GetId();

            employeeService.CompleteProfile(profile, userId);

            return RedirectToAction("Index", "Home");
        }
    }
}
