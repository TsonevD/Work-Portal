using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPortal.Infrastructure;
using WorkPortal.Services.Shifts;
using WorkPortal.Services.Shifts.Models;

namespace WorkPortal.Areas.Admin.Controllers
{
    using static AdminConstants;

    [Area(AreaName)]
    [Authorize(Roles = AdministratorRoleName)]
    public class ShiftController : Controller
    {
        private readonly IShiftService shiftService;

        public ShiftController(IShiftService shiftService)
            => this.shiftService = shiftService;

        
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ShiftInputServiceModel shift)
        {
            if (!User.IsAdmin())
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return View(shift);
            }
            shiftService.Add(shift);

            return RedirectToAction("All");
        }

        public IActionResult All()
        {
            var allShifts = shiftService.All();


            return View(allShifts);
        }

        public IActionResult Assign(int id)
        {
            var allEmployees = shiftService.AllEmployees();
            var shift = shiftService.FindShift(id);


            return View(new ShiftAssignModel()
            {
                Shift = shift,
                Employees = allEmployees,
            });
        }

        [HttpPost]
        public IActionResult Assign(int id, int employeeId)
        {
            ;
            if (!ModelState.IsValid)
            {
                return View();
            }
            shiftService.Assign(id, employeeId);

            return RedirectToAction("All");
        }





    }
}
