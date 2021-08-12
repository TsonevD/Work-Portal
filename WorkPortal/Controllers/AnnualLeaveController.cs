using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPortal.Infrastructure;
using WorkPortal.Models.AnnualLeave;
using WorkPortal.Services.AnnualLeaves;
using WorkPortal.Services.Employees;

namespace WorkPortal.Controllers
{
    public class AnnualLeaveController : Controller
    {
        private readonly IAnnualLeaveService annualLeaveService;
        private readonly IEmployeeService employeeService;

        public AnnualLeaveController(IAnnualLeaveService service, IEmployeeService employeeService)
        {
            this.annualLeaveService = service;
            this.employeeService = employeeService;
        }


        [Authorize]
        public IActionResult All()
        {
            var id = this.User.GetId();
            var isUserApproved = employeeService.IsUserApproved(id);
            
            if (id == null || !isUserApproved)
            {
                return Unauthorized();
            }
            var userId = employeeService.UserId(id);
            var all = annualLeaveService
                .Mine(userId);

            return View(all);
        }

        [Authorize]
        public IActionResult Add()
        {
            var isUserApproved = employeeService.IsUserApproved(this.User.GetId());
            if (!isUserApproved)
            {
                return Unauthorized();
            }
            return View();
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(AnnualLeaveInputModel annualLeave)
        {
            var isUserApproved = employeeService.IsUserApproved(this.User.GetId());

            if (!isUserApproved)
            {
                return Unauthorized();
            }
            var daysDifference = (annualLeave.EndDate.AddDays(1) - annualLeave.StartDate).TotalDays;

            if (annualLeave.EndDate < annualLeave.StartDate)
            {
                ModelState.AddModelError("FinishDate", "Finish Date needs to be after the Start Date!");
            }
            if (annualLeave.DaysToBeTaken > daysDifference)
            {
                ModelState.AddModelError("DaysToBeTaken", "You cannot take more days than the calendar difference.");
            }
            if (!ModelState.IsValid)
            {
                return View(annualLeave);
            }

            annualLeaveService.Add(annualLeave, this.User.GetId());

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var getId = this.User.GetId();

            var userId = employeeService.UserId(getId);

            var isUserApproved = employeeService.IsUserApproved(getId);
            if (!isUserApproved)
            {
                return Unauthorized();
            }

            var annualLeave = annualLeaveService.EditDetails(id, userId);

            return View(annualLeave);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, AnnualLeaveInputModel annualLeave)
        {
            var getId = User.GetId();
            var isUserApproved = employeeService.IsUserApproved(getId);
            if (!isUserApproved)
            {
                return Unauthorized();
            }

            var userId = employeeService.UserId(getId);

            var isByUser = annualLeaveService.IsByUser(id, userId);

            if (!isByUser && !User.IsAdmin())
            {
                return BadRequest();
            }

            annualLeaveService.Edit(id, annualLeave, userId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var getId = User.GetId();
            var isUserApproved = employeeService.IsUserApproved(getId);
            if (!isUserApproved)
            {
                return Unauthorized();
            }

            var userId = employeeService.UserId(getId);
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
