using Models;
using MyTested.AspNetCore.Mvc;
using System;
using System.Linq;
using WorkPortal.Areas.Admin;
using WorkPortal.Areas.Admin.Controllers;
using WorkPortal.Services.Shifts.Models;
using WorkPortal.Tests.Data;
using Xunit;

namespace WorkPortal.Tests.Controllers.Admin
{
    using static AdminConstants;

    public class ShiftControllerTest
    {

        [Fact]
        public void ControllerShouldBeInAdminArea()
              => MyController<ShiftController>
             .ShouldHave()
             .Attributes(attrs => attrs
            .SpecifyingArea(AreaName)
            .RestrictingForAuthorizedRequests(AdministratorRoleName));

        [Fact]
        public void AllShouldReturnCorrectView()
              => MyController<ShiftController>
                   .Instance(controller => controller
                       .WithUser(user => user.WithIdentifier(AreaName)
                       .InRole(AdministratorRoleName)))
                   .Calling(c => c.All())
                   .ShouldReturn()
                         .View();
        [Fact]
        public void AllShouldReturnUnAuthorizedWhenUserIsNotAdmin()
                 => MyController<ShiftController>
                 .Instance()
                 .WithUser()
                 .Calling(c => c.All())
                 .ShouldReturn()
                 .Unauthorized();
        
        [Fact]
        public void GetAddShouldReturnUnAuthorizedWhenUserIsNotAdmin()
              => MyController<ShiftController>
              .Instance()
              .WithUser()
              .Calling(c => c.Add())
              .ShouldReturn()
              .Unauthorized();

        [Fact]
        public void PostAddShouldReturnUnAuthorizedWhenUserIsNotAdmin()
          => MyController<ShiftController>
          .Instance()
          .WithUser()
          .Calling(c => c.Add(With.Any<ShiftInputServiceModel>()))
          .ShouldReturn()
          .Unauthorized();


        [Fact]
        public void GetAddShouldReturnCorrectView()
               =>
             MyController<ShiftController>
                    .Instance(controller => controller
                        .WithUser(user => user.WithIdentifier(AreaName)
                        .InRole(AdministratorRoleName)))
                    .Calling(c => c.Add())
                    .ShouldReturn()
                          .View();

        [Theory]
        [InlineData("14/08/2021", "10:00", "18:00", 8.30, 13.50, "First Avenue", 63, "W1W", "London", "H2B")]
        public void PostAddShouldRedirectAndReturnValidModel(
            string shiftDate,
            string startTime,
            string finishTime,
            decimal hoursWorking,
            decimal ratePerHour,
            string locationStreetName,
            int locationStreetNumber,
            string locationPostCode,
            string locationTownName,
            string locationCompanyName
            )
        => MyController<ShiftController>
            .Instance(controller => controller
                        .WithUser(user => user.WithIdentifier(AreaName)
                        .InRole(AdministratorRoleName)))
                    .Calling(c => c.Add(new ShiftInputServiceModel()
                    {
                        ShiftDate = DateTime.Parse(shiftDate),
                        StartTime = TimeSpan.Parse(startTime),
                        FinishTime = TimeSpan.Parse(finishTime),
                        HoursWorking = hoursWorking,
                        RatePerHour = ratePerHour,
                        Location = new LocationInputServiceModel()
                        {
                            StreetName = locationStreetName,
                            StreetNumber = locationStreetNumber,
                            PostCode = locationPostCode,
                            Town = locationTownName,
                            CompanyName = locationCompanyName,
                        }
                    }))
            .ShouldHave()
             .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post))
            .ValidModelState()
            .Data(data => data
            .WithSet<Shift>(shift => shift
            .Any(d =>
                        d.ShiftDate == DateTime.Parse(shiftDate) &&
                        d.StartTime == TimeSpan.Parse(startTime) &&
                        d.FinishTime == TimeSpan.Parse(finishTime) &&
                        d.HoursWorking == hoursWorking &&
                        d.RatePerHour == ratePerHour &&
                        d.Location.StreetName == locationStreetName &&
                        d.Location.StreetNumber == locationStreetNumber &&
                        d.Location.PostCode == locationPostCode &&
                        d.Location.Town == locationTownName &&
                        d.Location.CompanyName == locationCompanyName
            )))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect.To<ShiftController>(e => e.All()));

        [Fact]
        public void PostAddWithInvalidModelShouldReturnModel()
                 => MyController<ShiftController>
                     .Instance(controller => controller
                                 .WithUser(user => user.WithIdentifier(AreaName)
                                 .InRole(AdministratorRoleName)))
                             .Calling(c => c.Add(new ShiftInputServiceModel()))
                     .ShouldHave()
                      .ActionAttributes(attributes => attributes
                     .RestrictingForHttpMethod(HttpMethod.Post))
                     .InvalidModelState()
                     .AndAlso()
                     .ShouldReturn()
                     .View(view => view.WithModelOfType<ShiftInputServiceModel>());


        [Fact]
        public void GetAssignShouldReturnCorrectView()
        {
            var shift = MockedData.GetShift();
            
            MyController<ShiftController>
           .Instance(controller => controller
                      .WithUser(user => user.WithIdentifier(AreaName)
                      .InRole(AdministratorRoleName))
           .WithData(data => data.WithSet<Shift>(x => x.Add(shift))))
               .Calling(c => c.Assign(shift.Id))
                .ShouldReturn()
                .View();
        }


        [Fact]
        public void PostAssignShouldRedirectAndReturnCorrectModel()
        {
            var shift = MockedData.GetShift();
            var employee = MockedData.GetEmployee();

            MyController<ShiftController>
           .Instance(controller => controller
                       .WithUser(user => user.WithIdentifier(AreaName)
                       .InRole(AdministratorRoleName))
                       .WithData(data=>data.WithSet<Shift>(s =>s.Add(shift)))
                       .WithData(em=>em.WithSet<Employee>(e=>e.Add(employee))
                       ))
           .Calling(c => c.Assign(shift.Id, employee.Id))
           .ShouldHave()
             .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post))
            .ValidModelState()
            .Data(data => data
            .WithSet<Shift>(shift => shift
            .Any(d =>
            d.EmployeeId == employee.Id
            )))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect.To<ShiftController>(e => e.All()));
        }
    }
}
