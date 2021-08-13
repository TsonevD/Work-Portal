using System;
using Models;
using MyTested.AspNetCore.Mvc;
using WorkPortal.Controllers;
using Xunit;
using WorkPortal.Models.AnnualLeave;
using Models.Enums;
using System.Linq;
using WorkPortal.Tests.Data;

namespace WorkPortal.Tests.Controllers
{

    public class AnnualLeaveControllerTest
    {
        [Fact]
        public void AllShouldBeForApprovedUserAndReturnView()
        {
            var user = MockedData.GetApprovedUser();
            MyController<AnnualLeaveController>
                       .Instance()
                       .WithData(d=>d.WithSet<User>(u=>u.Add(user)))
                       .WithUser(u => u.WithIdentifier(user.Id))
                       .Calling(c => c.All())
                       .ShouldReturn()
                       .View();
        }

        [Fact]
        public void AllShouldReturnUnauthorizedForUnApprovedUser()
            => MyController<AnnualLeaveController>
                       .Instance()
                       .WithUser()
                       .Calling(c => c.All())
                       .ShouldReturn()
                       .Unauthorized();

        [Fact]
        public void GetAddShouldBeForApprovedUserAndReturnView()
        {
            var user = MockedData.GetApprovedUser();
            MyController<AnnualLeaveController>
                       .Instance()
                       .WithData(d => d.WithSet<User>(u => u.Add(user)))
                       .WithUser(u => u.WithIdentifier(user.Id))
                       .Calling(c => c.Add())
                       .ShouldReturn()
                       .View();
        }

        [Fact]
        public void GetAddShouldReturnUnauthorizedForUnApprovedUser()
         => MyController<AnnualLeaveController>
                    .Instance()
                    .WithUser()
                    .Calling(c => c.Add())
                    .ShouldReturn()
                    .Unauthorized();

        [Theory]
        [InlineData(AnnualLeaveType.PaidLeave , "14/09/2021" , "16/09/2021" , 1, "Doctor Appoitment")]
        public void PostAddShouldRedirectAndReturnCorrectModel(
            AnnualLeaveType annualLeaveType,
            string startDate,
            string endDate,
            int daysToBeTaken,
            string Reason)
        {
            var user = MockedData.GetApprovedUser();

            MyController<AnnualLeaveController>
                .Instance(controller=>controller
                .WithData(d=>d.WithSet<User>(u=>u.Add(user)))
                .WithData(d=>d.WithSet<Employee>(e => e.Add(MockedData.GetEmployee())))
               .WithUser(u=>u.WithIdentifier(user.Id)))
                .Calling(c=>c.Add(new AnnualLeaveInputModel
                {
                    LeaveType = annualLeaveType,
                    StartDate = DateTime.Parse(startDate),
                    EndDate = DateTime.Parse(endDate),
                    DaysToBeTaken = daysToBeTaken,
                    Reason = Reason,
                }))
                .ShouldHave()
                .ActionAttributes(atr=>atr
                .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(d=>d.WithSet<AnnualLeave>(a=>a
                .Any
                ( a=>
                        a.Type == annualLeaveType &&
                        a.StartDate == DateTime.Parse(startDate) &&
                        a.EndDate == DateTime.Parse(endDate) &&
                        a.DaysToBeTaken == daysToBeTaken &&
                        a.Reason == Reason
                    )))
                .AndAlso()
                .ShouldReturn()
                .Redirect(r=>r.To<AnnualLeaveController>(a=>a.All()));
        }

        [Fact]
        public void PostAddShouldReturnUnauthorizedForUnApprovedUser()
                => MyController<AnnualLeaveController>
                  .Instance()
                  .WithUser()
                  .Calling(c => c.Add(With.Any<AnnualLeaveInputModel>()))
                  .ShouldReturn()
                  .Unauthorized();

        [Fact]
        public void PostAddWithInvalidModelShouldReturnView()
        {
            var user = MockedData.GetApprovedUser();

                MyController<AnnualLeaveController>
                      .Instance(controller => controller
                  .WithData(d => d.WithSet<User>(u => u.Add(user)))
                  .WithData(d => d.WithSet<Employee>(e => e.Add(MockedData.GetEmployee())))
                 .WithUser(u => u.WithIdentifier(user.Id)))
                      .Calling(c => c.Add(new AnnualLeaveInputModel()))
                      .ShouldHave()
                       .ActionAttributes(attributes => attributes
                      .RestrictingForHttpMethod(HttpMethod.Post))
                      .InvalidModelState()
                      .AndAlso()
                      .ShouldReturn()
                      .View(view => view.WithModelOfType<AnnualLeaveInputModel>());
        }


        [Fact]
        public void GetEditShouldBeForApprovedUserAndReturnView()
        {
            var user = MockedData.GetApprovedUser();
            MyController<AnnualLeaveController>
                       .Instance()
                       .WithData(d=>d.WithSet<AnnualLeave>(a=>a.Add(MockedData.GetAnnualLeave())))
                       .WithData(d => d.WithSet<User>(u => u.Add(user)))
                       .WithUser(u => u.WithIdentifier(user.Id))
                       .Calling(c => c.Edit(1))
                       .ShouldReturn()
                       .View();
        }

        [Theory]
        [InlineData(AnnualLeaveType.SickLeave , "21/09/2021", "25/09/2021", 2, "Doctor Appoitment")]
        public void PostEditShouldRedirectAndReturnCorrectModel(
          AnnualLeaveType annualLeaveType,
          string startDate,
          string endDate,
          int daysToBeTaken,
          string Reason)
        {
            var user = MockedData.GetApprovedUser();

            MyController<AnnualLeaveController>
                .Instance(controller => controller
                .WithData(d => d.WithSet<User>(u => u.Add(user)))
                .WithData(d => d.WithSet<Employee>(e => e.Add(MockedData.GetEmployee())))
                .WithData(d=>d.WithSet<AnnualLeave>(a=>a.Add(MockedData.GetAnnualLeave())))
               .WithUser(u => u.WithIdentifier(user.Id)))
                .Calling(c => c.Edit(1 , new AnnualLeaveInputModel
                {
                    LeaveType = annualLeaveType,
                    StartDate = DateTime.Parse(startDate),
                    EndDate = DateTime.Parse(endDate),
                    DaysToBeTaken = daysToBeTaken,
                    Reason = Reason,
                }))
                .ShouldHave()
                .ActionAttributes(atr => atr
                .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(d => d.WithSet<AnnualLeave>(a => a
                  .Any
                  (a =>
                         a.Type == annualLeaveType &&
                         a.StartDate == DateTime.Parse(startDate) &&
                         a.EndDate == DateTime.Parse(endDate) &&
                         a.DaysToBeTaken == daysToBeTaken &&
                         a.Reason == Reason
                      )))
                .AndAlso()
                .ShouldReturn()
                .Redirect(r => r.To<AnnualLeaveController>(a => a.All()));
        }
        [Fact]
        public void GetEditShouldReturnUnauthorizedForUnApprovedUser()
                => MyController<AnnualLeaveController>
                  .Instance()
                  .WithUser()
                  .Calling(c => c.Edit(1))
                  .ShouldReturn()
                  .Unauthorized();

        [Fact]
        public void PostEditShouldReturnUnauthorizedForUnApprovedUser()
               => MyController<AnnualLeaveController>
                 .Instance()
                 .WithUser()
                 .Calling(c => c.Edit(1 , With.Any<AnnualLeaveInputModel>()))
                 .ShouldReturn()
                 .Unauthorized();

        [Fact]
        public void PostEditWithInvalidModelShouldReturnView()
        {
            var user = MockedData.GetApprovedUser();

            MyController<AnnualLeaveController>
                  .Instance(controller => controller
              .WithData(d => d.WithSet<User>(u => u.Add(user)))
              .WithData(d => d.WithSet<Employee>(e => e.Add(MockedData.GetEmployee())))
              .WithData(d=> d.WithSet<AnnualLeave>(a=>a.Add(MockedData.GetAnnualLeave())))
             .WithUser(u => u.WithIdentifier(user.Id)))
                  .Calling(c => c.Edit(1 , new AnnualLeaveInputModel() 
                  { 
                      Reason = "a"
                  }))
                  .ShouldHave()
                   .ActionAttributes(attributes => attributes
                  .RestrictingForHttpMethod(HttpMethod.Post))
                  .InvalidModelState()
                  .AndAlso()
                  .ShouldReturn()
                  .View(view => view.WithModelOfType<AnnualLeaveInputModel>());
        }
        [Fact]
        public void PostEditShouldReturnBadRequestWhenAnnualLeaveIsNotByTheUser()
        {
            var user = MockedData.GetApprovedUser();

            MyController<AnnualLeaveController>
                  .Instance(controller => controller
              .WithData(d => d.WithSet<User>(u => u.Add(user)))
              .WithData(d => d.WithSet<Employee>(e => e.Add(MockedData.GetEmployee())))
             .WithUser(u => u.WithIdentifier(user.Id)))
                  .Calling(c => c.Edit(3, With.Any<AnnualLeaveInputModel>()))
                  .ShouldHave()
                   .ActionAttributes(attributes => attributes
                  .RestrictingForHttpMethod(HttpMethod.Post))
                  .ValidModelState()
                  .AndAlso()
                  .ShouldReturn()
                  .BadRequest();
        }


        [Fact]
        public void DeleteShouldRemoveEntryAndRedirect()
        {
            var user = MockedData.GetApprovedUser();
            MyController<AnnualLeaveController>
               .Instance(controller => controller
               .WithData(d => d.WithSet<User>(u => u.Add(user)))
               .WithData(d => d.WithSet<Employee>(e => e.Add(MockedData.GetEmployee())))
               .WithData(d => d.WithSet<AnnualLeave>(a => a.Add(MockedData.GetAnnualLeave())))
              .WithUser(u => u.WithIdentifier(user.Id)))
               .Calling(c=>c.Delete(1))
               .ShouldReturn()
               .Redirect(r=>r.To<AnnualLeaveController>(a=>a.All()));
        }

        [Fact]
        public void DeleteShouldReturnUnauthorizedForUnApprovedUser()
              => MyController<AnnualLeaveController>
                .Instance()
                .WithUser()
                .Calling(c => c.Delete(1))
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void DeleteShouldReturnBadRequestWhenAnnualLeaveIsNotByTheUser()
        {
            var user = MockedData.GetApprovedUser();

            MyController<AnnualLeaveController>
                  .Instance(controller => controller
              .WithData(d => d.WithSet<User>(u => u.Add(user)))
              .WithData(d => d.WithSet<Employee>(e => e.Add(MockedData.GetEmployee())))
             .WithUser(u => u.WithIdentifier(user.Id)))
                  .Calling(c => c.Delete(3))
                  .ShouldReturn()
                  .BadRequest();
        }
    }
}
