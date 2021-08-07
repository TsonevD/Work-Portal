using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using WorkPortal.Data;
using WorkPortal.Infrastructure;
using WorkPortal.Models.Shifts;
using WorkPortal.Services.Employees;
using WorkPortal.Services.Shifts;

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
        public IActionResult Mine(ShiftViewModel query)
        {
            var id = this.User.GetId();
            var userId = this.employeeService.UserId(id);

            var mineShifts = this.shiftService.Mine(userId);

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
