using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPortal.Areas.Admin.Models.Employee;
using WorkPortal.Data;
using WorkPortal.Services.Employees;

namespace WorkPortal.Areas.Admin.Controllers
{
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }


        public IActionResult All()
        {
            var allUsers = employeeService.All();

            return View(allUsers);
        }

        public IActionResult Add()
        {
            var departments = employeeService.GetDepartments();

            var managers = employeeService.GetManagers();

            var view = new AddEmployeeInputModel()
            {
                Departments = departments,
                Managers = managers,
            };

            return View(view);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeInputModel employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            await employeeService.AdminAddUser(employee);

            return RedirectToAction("All");
        }

        public IActionResult Approve(string id)
        {
            var employee = employeeService.FindUser(id);

            if (employee == null)
            {
                return BadRequest();
            }

            employeeService.AdminApproveUser(employee);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(string id)
        {
            var employee = employeeService.FindUser(id);

            if (employee == null)
            {
                return BadRequest();
            }
            employeeService.AdminDeleteUser(employee);

            return RedirectToAction(nameof(All));
        }
    }
}
