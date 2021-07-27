using Microsoft.AspNetCore.Mvc;
using WorkPortal.Models.AnnualLeave;

namespace WorkPortal.Controllers
{
    public class AnnualLeaveController : Controller
    {

        public IActionResult All()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AnnualLeaveInputModel annualLeave)
        {
            ;
            if (annualLeave.EndDate < annualLeave.StartDate)
            {
                ModelState.AddModelError("FinishDate", "Finish Date needs to be after the Start Date!");
            }
            if (!ModelState.IsValid)
            {
                return View(annualLeave);
            }


            return RedirectToAction(nameof(All));
        }
        public IActionResult Data()
        {
            return View();
        }
    }
}
