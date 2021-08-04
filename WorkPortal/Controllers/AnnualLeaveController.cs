using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using WorkPortal.Data;
using WorkPortal.Infrastructure;
using WorkPortal.Models.AnnualLeave;

namespace WorkPortal.Controllers
{
    public class AnnualLeaveController : Controller
    {
        private readonly WorkPortalDbContext data;

        public AnnualLeaveController(WorkPortalDbContext data)
        {
            this.data = data;
        }


        [Authorize]
        public IActionResult All()
        {
            var allAnnualLeave = this.data.Employees.Where(x => x.UserId == this.User.GetId())
                .Select(x => new AllAnnualLeaveViewModel()
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    AnnualLeave = x.AnnualLeaves.Select(a => new AnnualLeaveModel()
                    {
                        Id = a.Id,
                        DaysToBeTaken = (int)a.DaysToBeTaken,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        Status = a.Status,
                        Reason = a.Reason,
                        LeaveType = a.Type,
                    }).ToList(),
                }).FirstOrDefault();

            return View(allAnnualLeave);
        }

        public IActionResult Add()
            => View();

        [HttpPost]
        [Authorize]
        public IActionResult Add(AnnualLeaveInputModel annualLeave)
        {
            var id = this.User.GetId();
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

            var employee = this.data.Employees.First(x => x.UserId == id);

            var newLeaveRequest = new AnnualLeave()
            {
                EmployeeId = employee.Id,
                StartDate = annualLeave.StartDate,
                EndDate = annualLeave.EndDate,
                Reason = annualLeave.Reason,
                Type = annualLeave.LeaveType,
                DaysToBeTaken = annualLeave.DaysToBeTaken,
            };
            this.data.AnnualLeaves.Add(newLeaveRequest);

            employee.AnnualLeaves.Add(newLeaveRequest);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var idByUser = this.data
                .Employees
                .Where(d => d.UserId == this.User.GetId())
                .Select(d => d.Id)
                .FirstOrDefault();

            var annualLeave = this.data.AnnualLeaves
                .Where(x => x.Id == id && x.EmployeeId==idByUser)
                .Select(a => new AnnualLeaveEditModel()
                {
                    Id = a.EmployeeId,
                    DaysToBeTaken = (int)a.DaysToBeTaken,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate,
                    Reason = a.Reason,
                    LeaveType = a.Type,
                }).FirstOrDefault();

            return View(annualLeave);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, AnnualLeaveInputModel annualLeave)
        {
            var userId = User.GetId();

            var idByUser = this.data
                .Employees
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

            var userAnnualLeave = this.data.AnnualLeaves.FirstOrDefault(x => x.Id== id && x.EmployeeId==idByUser);

            if (userAnnualLeave == null || !User.IsAdmin())
            {
                return BadRequest();
            }

            userAnnualLeave.StartDate = annualLeave.StartDate;
            userAnnualLeave.EndDate = annualLeave.EndDate;
            userAnnualLeave.Type = annualLeave.LeaveType;
            userAnnualLeave.Reason= annualLeave.Reason;
            userAnnualLeave.DaysToBeTaken = annualLeave.DaysToBeTaken;

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var idByUser = this.data
                .Employees
                .Where(d => d.UserId == this.User.GetId())
                .Select(d => d.Id)
                .FirstOrDefault();

            var annualLeave = this.data.AnnualLeaves
                .FirstOrDefault(x => x.Id == id && x.EmployeeId == idByUser);

            if (annualLeave == null)
            {
                return BadRequest();
            }

            this.data.AnnualLeaves.Remove(annualLeave);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

    }
}
