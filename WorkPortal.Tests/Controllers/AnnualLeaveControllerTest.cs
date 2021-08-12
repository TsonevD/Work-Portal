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
        public void AllhouldBeForApprovedUserAndReturnView()
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
        public void AddShouldBeForApprovedUserAndReturnView()
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

        [Theory]
        [InlineData(AnnualLeaveType.PaidLeave , "14/09/2021" , "16/09/2021" , 1, "Doctor Appoitment")]
        public void AddShouldRedirectAndReturnCorrectModel(
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
        public void EditShouldBeForApprovedUserAndReturnView()
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
        public void EditShouldRedirectAndReturnCorrectModel(
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
    }
}
