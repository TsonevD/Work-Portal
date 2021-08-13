using Models.Enums;
using MyTested.AspNetCore.Mvc;
using WorkPortal.Areas.Admin;
using WorkPortal.Areas.Admin.Controllers;
using WorkPortal.Areas.Admin.Models.Employee;
using Xunit;
using System;
using Models;
using System.Linq;
using WorkPortal.Tests.Data;

namespace WorkPortal.Tests.Controllers.Admin
{
    using static AdminConstants;

    public class EmployeeControllerTest
    {
        [Fact]
        public void ControllerShouldBeInAdminArea()
            => MyController<EmployeeController>
                .ShouldHave()
                .Attributes(attrs => attrs
                    .SpecifyingArea(AreaName)
                    .RestrictingForAuthorizedRequests(AdministratorRoleName));

        [Fact]
        public void AllShouldReturnView()
                 =>
             MyController<EmployeeController>
             .Instance()
             .Calling(c => c.All())
             .ShouldReturn()
             .View();

        [Fact]
        public void GetAddShouldReturnView()
                     =>
                 MyController<EmployeeController>
                 .Instance()
                 .Calling(c => c.Add())
                 .ShouldReturn()
                 .View();

        [Theory]
        [InlineData("Dimitar", "Tsonev", "0888123456", "16/01/1991", "asd@abv.bg", Gender.Male, "https://i49.vbox7.com/o/577/577f407c3b0.jpg",
            "14/06/2021", "Waiter", "First Avenue", "W1W", "London", 1, 1)]
        public void PostAddShouldRedirectAndReturnValidModel(
            string firstName,
            string lastName,
            string phone,
            string dateOfbirth,
            string email,
            Gender gender,
            string imageUrl,
            string hireDate,
            string jobTitle,
            string streetName,
            string postCode,
            string townName,
            int departmentId,
            int managerId)
            => MyController<EmployeeController>
            .Instance()
            .WithUser()
            .Calling(c => c.Add(new AddEmployeeInputModel
            {
                Firstname = firstName,
                Lastname = lastName,
                Phone = phone,
                DateOfBirth = DateTime.Parse(dateOfbirth),
                Email = email,
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
            .RestrictingForHttpMethod(HttpMethod.Post))
            .ValidModelState()
            .Data(data => data
            .WithSet<Employee>(user => user
            .Any(d =>
                    d.Phone == phone &&
                    d.Gender == gender &&
                    d.ProfilePictureUrl == imageUrl &&
                    d.HireDate == DateTime.Parse(hireDate) &&
                    d.JobTitle == jobTitle &&
                    d.Address.StreetName == streetName &&
                    d.Address.PostCode == postCode &&
                    d.Address.Town.Name == townName &&
                    d.DepartmentId == departmentId &&
                    d.ManagerId == managerId &&

                    d.User.FirstName == firstName &&
                    d.User.LastName == lastName &&
                    d.User.Email == email &&
                    d.User.DateOfBirth == DateTime.Parse(dateOfbirth)
            )))
            .AndAlso()
            .ShouldReturn()
            .Redirect(redirect => redirect.To<EmployeeController>(e => e.All()));

        [Theory]
        [InlineData("Dimitar", "Tsonev")]
        public void PostAddShouldThrowExceptionWhenModelInvalidAndReturnView(
                 string firstName,
                 string lastName
                 )
                 => MyController<EmployeeController>
                 .Instance()
                 .WithUser()
                 .Calling(c => c.Add(new AddEmployeeInputModel
                 {
                     Firstname = firstName,
                     Lastname = lastName,
                 }))
                    .ShouldHave()
                     .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                    .InvalidModelState()
                    .AndAlso()
                    .ShouldReturn()
                    .View(view => view
                    .WithModelOfType<AddEmployeeInputModel>());

        [Fact]
        public void ApproveShouldRedirectWithValidInformation()
        {
            var user = MockedData.GetApprovedUser();

            MyController<EmployeeController>
                .Instance()
                .WithUser()
                .WithData(data => data.WithSet<User>(x => x.Add(user)))
                .Calling(c => c.Approve(user.Id))
                .ShouldReturn()
                .Redirect(redirect => redirect.To<EmployeeController>(e => e.All())); ;
        }

        [Fact]
        public void ApproveShouldThrowBadRequestWhenEmployeeIsNull()
        {
            MyController<EmployeeController>
                .Instance()
                .WithUser()
                .Calling(c => c.Approve("1"))
                .ShouldReturn()
                .BadRequest();
        }

        [Fact]
        public void DeleteShouldRedirectWithValidInformation()
        {
            var user = MockedData.GetApprovedUser();

            MyController<EmployeeController>
                .Instance()
                .WithUser()
                .WithData(data => data.WithSet<User>(x => x.Add(user)))
                .Calling(c => c.Delete(user.Id))
                .ShouldReturn()
                .Redirect(redirect => redirect.To<EmployeeController>(e => e.All())); ;
        }
        [Fact]
        public void DeleteShouldThrowBadRequestWhenEmployeeIsNull()
        {
            MyController<EmployeeController>
                .Instance()
                .WithUser()
                .Calling(c => c.Delete("1"))
                .ShouldReturn()
                .BadRequest();
        }
    }
}
