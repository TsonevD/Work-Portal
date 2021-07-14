using Microsoft.AspNetCore.Mvc;
using WorkPortal.Data;

namespace WorkPortal.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly WorkPortalDbContext data;

        public EmployeeController(WorkPortalDbContext data) 
            => this.data = data;

        public IActionResult Portal()
        {
            return View();
        }

    }
}
