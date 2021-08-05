using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPortal.Infrastructure;
using WorkPortal.Models.AnnualLeave;
using WorkPortal.Services.AnnualLeave;

namespace WorkPortal.Controllers
{
    public class AnnualLeaveController : Controller
    {
        private readonly IAnnualLeaveService annualLeaveService;

        public AnnualLeaveController(IAnnualLeaveService service)
            => this.annualLeaveService = service;


        [Authorize]
        public IActionResult All()
        {
            var userId = this.User.GetId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var all = annualLeaveService.All(userId);

            return View(all);
        }

        [Authorize]
        public IActionResult Add()
            => View();


        [HttpPost]
        [Authorize]
        public IActionResult Add(AnnualLeaveInputModel annualLeave)
        {
            var daysDifference = (annualLeave.EndDate-annualLeave.StartDate).TotalDays;
            
            if (annualLeave.EndDate < annualLeave.StartDate)
            {
                ModelState.AddModelError("FinishDate", "Finish Date needs to be after the Start Date!");
            }
            if (annualLeave.DaysToBeTaken > daysDifference)
            {
                ModelState.AddModelError("DaysToBeTaken" , "You cannot take more days than the calendar difference.");
            }
            if (!ModelState.IsValid)
            {
                return View(annualLeave);
            }

            annualLeaveService.Add(annualLeave , this.User.GetId());

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = annualLeaveService.UserId(this.User.GetId());

            var annualLeave = annualLeaveService.EditDetails(id, userId);

            return View(annualLeave);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, AnnualLeaveInputModel annualLeave)
        {
            var userId = annualLeaveService.UserId(User.GetId());
            var isByUser = annualLeaveService.IsByUser(id, userId);

            if (!isByUser && !User.IsAdmin())
            {
                return BadRequest();
            }

            annualLeaveService.Edit(id , annualLeave, userId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = annualLeaveService.UserId(User.GetId());
            var isByUser = annualLeaveService.IsByUser(id, userId);

            if (!isByUser)
            {
                return BadRequest();
            }

            annualLeaveService.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
