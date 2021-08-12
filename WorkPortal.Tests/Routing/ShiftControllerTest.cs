using MyTested.AspNetCore.Mvc;
using WorkPortal.Controllers;
using WorkPortal.Services.Shifts.Models;
using Xunit;


namespace WorkPortal.Tests.Routing
{
   public class ShiftControllerTest
    {
        [Fact]
        public void MineShouldBeMapped()
              => MyRouting
                  .Configuration()
                  .ShouldMap(request => request
                      .WithPath("/Shifts/Mine"))
                  .To<ShiftsController>(c => c.Mine(With.Any<ShiftQueryModel>()));


        [Fact]
        public void DetailsShouldBeMapped()
          => MyRouting
              .Configuration()
              .ShouldMap(request => request
                  .WithPath("/Shifts/Details/1"))
              .To<ShiftsController>(c => c.Details(1));
    }
}
