using MyTested.AspNetCore.Mvc;
using WorkPortal.Areas.Admin;
using WorkPortal.Areas.Admin.Controllers;
using Xunit;

namespace WorkPortal.Tests.Controllers.Admin
{
    using static AdminConstants;

    public class PayslipControllerTest
    {
        [Fact]
        public void ControllerShouldBeInAdminArea()
            => MyController<PayslipController>
                .ShouldHave()
                .Attributes(attrs => attrs
                    .SpecifyingArea(AreaName)
                    .RestrictingForAuthorizedRequests(AdministratorRoleName));

        [Fact]
        public void EmployeesShouldReturnCorrectView()
            =>
                MyController<PayslipController>
                    .Instance(controller => controller
                        .WithUser(user => user.WithIdentifier(AreaName)
                            .InRole(AdministratorRoleName)))
                    .Calling(c => c.Employees())
                    .ShouldReturn()
                    .View();

        [Fact]
        public void EmployeesShouldReturnUnAuthorizedWhenUserIsNotAdmin()
            => MyController<PayslipController>
                .Instance()
                .WithUser()
                .Calling(c => c.Employees())
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void AllShouldReturnCorrectView()
            =>
                MyController<PayslipController>
                    .Instance(controller => controller
                        .WithUser(user => user.WithIdentifier(AreaName)
                            .InRole(AdministratorRoleName)))
                    .Calling(c => c.All(4 , 7))
                    .ShouldReturn()
                    .View();

        [Fact]
        public void AllShouldReturnUnAuthorizedWhenUserIsNotAdmin()
            => MyController<PayslipController>
                .Instance()
                .WithUser()
                .Calling(c => c.All(4,7))
                .ShouldReturn()
                .Unauthorized();

        [Fact]
        public void GenerateShouldRedirectCorrectly()
            =>
                MyController<PayslipController>
                    .Instance(controller => controller
                        .WithUser(user => user.WithIdentifier(AreaName)
                            .InRole(AdministratorRoleName)))
                    .Calling(c => c.Generate(4, 7))
                    .ShouldReturn()
                    .RedirectToAction("Employees");


        [Fact]
        public void GenerateShouldReturnUnAuthorizedWhenUserIsNotAdmin()
            => MyController<PayslipController>
                .Instance()
                .WithUser()
                .Calling(c => c.Generate(4, 7))
                .ShouldReturn()
                .Unauthorized();
    }
}
