using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkPortal.Infrastructure;
using WorkPortal.Services.Employees;
using WorkPortal.Services.Shifts;
using WorkPortal.Services.Shifts.Models;

namespace WorkPortal.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly IShiftService shiftService;
        private readonly IEmployeeService employeeService;

        public ShiftsController(IShiftService shiftService, IEmployeeService employeeService)
        {
            this.shiftService = shiftService;
            this.employeeService = employeeService;
        }

        [Authorize]
        public IActionResult Mine(ShiftQueryModel query)
        {
            var id = this.User.GetId();
            var userId = this.employeeService.UserId(id);

            var mineShifts = this.shiftService
                .Mine(userId);

            return View(mineShifts);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var shift = shiftService.Details(id);
           
            return View(shift);
        }
    }
}
