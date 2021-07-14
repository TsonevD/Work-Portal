using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorkPortal.Data;
using WorkPortal.Models.Employee;

namespace WorkPortal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly WorkPortalDbContext data;

        public EmployeeController(WorkPortalDbContext data) 
            => this.data = data;

        public IActionResult Portal()
        {
            var id = 1;
            var profile = this.data.Employees.Where(x => x.Id == id)
                .Select(x => new ProfileViewModel()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    HireDate = x.HireDate,
                    JobTitle = x.JobTitle,
                    Id = x.Id,
                    CompanyName = x.Department.Company.Name,
                    CompanyLocation = x.Department.Company.Town,
                    ImageUrl = x.ProfilePictureUrl,
                }).FirstOrDefault();

            return View(profile);
        }

    }
}
