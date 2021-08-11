using Models;
using MyTested.AspNetCore.Mvc;
using WorkPortal.Areas.Admin;
using WorkPortal.Areas.Admin.Controllers;
using Xunit;

namespace WorkPortal.Tests.Controllers.Admin
{
    using static AdminConstants;

    public class AnnualLeaveControllerTest
    {
        [Fact]
        public void ControllerShouldBeInAdminArea()
            => MyController<AnnualLeaveController>
                .ShouldHave()
                .Attributes(attrs => attrs
                    .SpecifyingArea(AreaName)
                    .RestrictingForAuthorizedRequests(AdministratorRoleName));
        [Fact]
        public void AllShouldReturnCorrectView()
            =>
                MyController<AnnualLeaveController>
            .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(AreaName)
                .InRole(AdministratorRoleName)))
            .Calling(c => c.All())
            .ShouldReturn()
            .View();

        [Fact]
        public void ApproveShouldRedirectWithValidModel()
            =>
                MyController<AnnualLeaveController>
            .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(AreaName)
                .InRole(AdministratorRoleName))
            .WithData(data => data.WithSet<AnnualLeave>(a => a.Add(new AnnualLeave()
            {
                Id = 1,
            }
            ))))
            .Calling(c => c.Approve(1))
            .ShouldReturn()
            .Redirect(redirect => redirect.To<AnnualLeaveController>(a => a.All()));

        [Fact]
        public void DeclineShouldRedirectWithValidModel()
              =>
                  MyController<AnnualLeaveController>
              .Instance(controller => controller
                  .WithUser(user => user.WithIdentifier(AreaName)
                  .InRole(AdministratorRoleName))
              .WithData(data => data.WithSet<AnnualLeave>(a => a.Add(new AnnualLeave()
              {
                  Id = 1,
              }
              ))))
              .Calling(c => c.Decline(1))
              .ShouldReturn()
              .Redirect(redirect => redirect.To<AnnualLeaveController>(a => a.All()));

    }
}
