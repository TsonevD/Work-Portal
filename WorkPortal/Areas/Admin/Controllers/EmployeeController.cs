using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPortal.Areas.Admin.Models.Employee;
using WorkPortal.Data;

namespace WorkPortal.Areas.Admin.Controllers
{
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class EmployeeController : Controller
    {
        private readonly WorkPortalDbContext data;

        public EmployeeController(WorkPortalDbContext data)
            => this.data = data;


        public IActionResult All()
        {
            var allUsers = this.data.Users.Where(x => x.IsApproved==false)
                .Select(x => new AllEmployeeViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DateOfBirth = x.DateOfBirth,
                    Email = x.Email,
                    IsApproved = x.IsApproved,
                }).ToList();

            return View(allUsers);
        }

        public IActionResult Approve(string id)
        {
            var employee = this.data.Users.FirstOrDefault(x => x.Id == id);

            if (employee == null)
            {
                return BadRequest();
            }

            employee.IsApproved = true;
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));

        }

        public IActionResult Add(AddViewModel employee)
        {

            return View();
        }


        public IActionResult Delete(string id)
        {
            var employee = this.data.Users.FirstOrDefault(x => x.Id == id);

            if (employee == null)
            {
                return BadRequest();
            }

            this.data.Users.Remove(employee);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
    }
}
