using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using WorkPortal.Areas.Admin.Models.Employee;
using WorkPortal.Data;
using WorkPortal.Infrastructure;

namespace WorkPortal.Areas.Admin.Controllers
{
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class EmployeeController : Controller
    {
        private readonly WorkPortalDbContext data;
        private readonly UserManager<User> userManager;

        public EmployeeController(WorkPortalDbContext data, UserManager<User> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }


        public IActionResult All()
        {
            var allUsers = this.data.Users.Where(x => x.IsApproved == false)
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

        public IActionResult Add()
        {
            var departments = this.data.Departments
                .Select(x => new DepartmentViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

            var managers = this.data.Employees
                .Where(x => x.ManagerId == null)
                .Select(x => new ManagersViewModel()
                {
                    Id = x.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                })
                .ToList();

            var view = new AddEmployeeViewModel()
            {
                Departments = departments,
                Managers = managers,
            };

            return View(view);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel employee)
        {
            ;
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            var pass = "asd123";
            var user = new User
            {
                UserName = employee.Email,
                Email = employee.Email,
                FirstName = employee.Firstname,
                LastName = employee.Lastname,
                DateOfBirth = employee.DateOfBirth,
                IsApproved = true,
            };

            var result = await this.userManager.CreateAsync(user, pass);

            var newEmployee = new Employee()
            {
                MiddleName = employee.MiddleName,
                Gender = employee.Gender,
                HireDate = employee.HireDate,
                JobTitle = employee.JobTitle,
                ManagerId = employee.ManagerId,
                DepartmentId = employee.DepartmentId,
                Phone = employee.Phone,
                ProfilePictureUrl = employee.ImageUrl,
                UserId = user.Id,
                Address = new Address()
                {
                    PostCode = employee.PostCode,
                    StreetName = employee.StreetName,
                    Town = new Town()
                    {
                        Name = employee.TownName,
                    },
                },
            };
            this.data.Employees.Add(newEmployee);

            await this.data.SaveChangesAsync();

            return RedirectToAction("All");
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

        public IActionResult Delete(string id)
        {
            var employee = this.data.Users
                .FirstOrDefault(x => x.Id == id);

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
