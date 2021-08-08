using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPortal.Infrastructure;
using WorkPortal.Services.AnnualLeaves;

namespace WorkPortal.Areas.Admin.Controllers
{
    using static AdminConstants;


    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class AnnualLeaveController : Controller
    {
        private readonly IAnnualLeaveService annualLeaveService;

        public AnnualLeaveController(IAnnualLeaveService annualLeaveService)
            => this.annualLeaveService = annualLeaveService;

   
        public IActionResult All()
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }
            var all = this.annualLeaveService
                .All();

            return View(all);
        }


        public IActionResult Approve(int id)
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            this.annualLeaveService.Approve(id);

            return RedirectToAction("All");
        }

        public IActionResult Decline(int id)
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            this.annualLeaveService.Decline(id);

            return RedirectToAction("All");
        }
    }
}
