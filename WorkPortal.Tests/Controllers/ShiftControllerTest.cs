using Models;
using MyTested.AspNetCore.Mvc;
using WorkPortal.Controllers;
using WorkPortal.Services.Shifts.Models;
using WorkPortal.Tests.Data;
using Xunit;

namespace WorkPortal.Tests.Controllers
{
    public class ShiftControllerTest
    {
        [Fact]
        public void MineShouldBeForApprovedUserAndReturnView()
        {
            var user = MockedData.GetApprovedUser();
            MyController<ShiftsController>
                       .Instance()
                       .WithData(d => d.WithSet<User>(u => u.Add(user)))
                       .WithData(d => d.WithSet<Shift>(u => u.Add(MockedData.GetShift())))
                       .WithUser(u => u.WithIdentifier(user.Id))
                       .Calling(c => c.Mine(With.Any<ShiftQueryModel>()))
                       .ShouldReturn()
                       .View();
        }

        [Fact]
        public void DetailsShouldBeForApprovedUserAndReturnView()
        {
            var user = MockedData.GetApprovedUser();
            MyController<ShiftsController>
                       .Instance()
                       .WithData(d => d.WithSet<User>(u => u.Add(user)))
                       .WithData(d => d.WithSet<Shift>(u => u.Add(MockedData.GetShift())))
                       .WithUser(u => u.WithIdentifier(user.Id))
                       .Calling(c => c.Details(1))
                       .ShouldReturn()
                       .View();
        }
    }
}
