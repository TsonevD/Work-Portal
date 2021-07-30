using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPortal.Data;
using WorkPortal.Infrastructure;
using WorkPortal.Models.Employee;

namespace WorkPortal.Controllers
{
    public class EmployeeController : Controller 
    {
        private readonly WorkPortalDbContext data;

        public EmployeeController(WorkPortalDbContext data) 
            => this.data = data;

        [Authorize]
        public IActionResult Portal()
        {
            var id = this.User.GetId();

            //var profile = this.data.Employees.Where(x => x.UserId == id)
            //    .Select(x => new ProfileViewModel()
            //    {
            //        FirstName = x.FirstName,
            //        LastName = x.LastName,
            //        HireDate = x.HireDate,
            //        JobTitle = x.JobTitle,
            //        Id = x.Id,
            //        CompanyName = x.Department.Company.Name,
            //        CompanyLocation = x.Department.Company.Town,
            //        ImageUrl = x.ProfilePictureUrl,
            //    }).FirstOrDefault();

            return View();
        }

    }
}
