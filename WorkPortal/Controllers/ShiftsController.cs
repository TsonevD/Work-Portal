using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkPortal.Data;
using WorkPortal.Models.Shifts;

namespace WorkPortal.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly WorkPortalDbContext data;

        public ShiftsController(WorkPortalDbContext data)
        {
            this.data = data;
        }

        public IActionResult All(AllShiftsViewModel query)
        {
            var id = 2;

            var shifts = this.data.Shifts
                .Where(x => x.Employees.Any(e => e.Id == id))
                .Select(x => new ShiftViewModel()
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    RatePerHour = x.RatePerHour,
                    FinishTime = x.FinishTime,
                    HoursWorking = x.HoursWorking,
                    ShiftDate = x.ShiftDate,
                    Location = new LocationViewModel()
                    {
                        PostCode = x.Location.PostCode,
                        StreetName = x.Location.StreetName,
                        StreetNumber = x.Location.StreetNumber,
                        Town = x.Location.Town,
                        CompanyName = x.Location.CompanyName,
                    },
                }).ToList();

            query.Shifts = shifts;

            return View(query);
        }


        public IActionResult Details(int id)
        {
            var shift = this.data.Shifts
                .Where(x=>x.Id==id)
                .Select(x => new ShiftViewModel()
                {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    RatePerHour = x.RatePerHour,
                    FinishTime = x.FinishTime,
                    HoursWorking = x.HoursWorking,
                    ShiftDate = x.ShiftDate,
                    Location = new LocationViewModel()
                    {
                        PostCode = x.Location.PostCode,
                        StreetName = x.Location.StreetName,
                        StreetNumber = x.Location.StreetNumber,
                        Town = x.Location.Town,
                        CompanyName = x.Location.CompanyName,
                    },
                }).FirstOrDefault();

            return View(shift);
        }
    }
}
