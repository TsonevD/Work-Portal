using Models;
using Models.Enums;
using MyTested.AspNetCore.Mvc;
using System;
using System.Linq;
using WorkPortal.Controllers;
using WorkPortal.Services.Employees;
using WorkPortal.Services.Employees.Models;
using Xunit;

namespace WorkPortal.Tests.Controllers
{
    public class EmployeeControllerTest
    {
        [Fact]
        public void MyProfileShouldBeForAuthorizeUserAndReturnView()
            =>
            MyController<EmployeeController>
            .Instance()
            .Calling(c => c.Profile())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Fact]
        public void GetCompleteProfileShouldBeForAuthorizeUserAndReturnView()
             =>
             MyController<EmployeeController>
             .Instance()
             .Calling(c => c.CompleteProfile())
             .ShouldHave()
             .ActionAttributes(attributes => attributes
             .RestrictingForAuthorizedRequests())
             .AndAlso()
             .ShouldReturn()
             .View();

        [Theory]
        [InlineData("0888123456", Gender.Male, "https://i49.vbox7.com/o/577/577f407c3b0.jpg", "14/06/2021", "Waiter", "First Avenue", "W1W", "London", 1, 1)]
        public void PostCompleteProfileShouldBeForAuthorizedAndReturectWithValidModel(
            string phone,
            Gender gender,
            string imageUrl,
            string hireDate,
            string jobTitle,
            string streetName,
            string postCode,
            string townName,
            int departmentId,
            int managerId
            )
            =>
            MyController<EmployeeController>
            .Instance(controller => controller
            .WithUser())
            .Calling(c => c.CompleteProfile(new ProfileServiceModel
            {
                Phone = phone,
                Gender = gender,
                ImageUrl = imageUrl,
                HireDate = DateTime.Parse(hireDate),
                JobTitle = jobTitle,
                StreetName = streetName,
                PostCode = postCode,
                TownName = townName,
                DepartmentId = departmentId,
                ManagerId = managerId,
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
            .RestrictingForHttpMethod(HttpMethod.Post)
            .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
            .WithSet<Employee>(employee => employee
            .Any(d =>
                    d.Phone == phone &&
                    d.Gender == gender &&
                    d.ProfilePictureUrl == imageUrl &&
                    d.HireDate == DateTime.Parse(hireDate) &&
                    d.JobTitle == jobTitle &&
                    d.Address.StreetName== streetName &&
                    d.Address.PostCode == postCode &&
                    d.Address.Town.Name == townName &&
                    d.DepartmentId == departmentId &&
                    d.ManagerId == managerId
            )))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect.To<HomeController>(e=>e.Index()));




    }
}
