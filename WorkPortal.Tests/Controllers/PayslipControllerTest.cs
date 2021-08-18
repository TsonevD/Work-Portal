using Models;
using MyTested.AspNetCore.Mvc;
using WorkPortal.Controllers;
using WorkPortal.Tests.Data;
using Xunit;
namespace WorkPortal.Tests.Controllers
{
    public class PayslipControllerTest
    {
        [Fact]
        public void MineShouldBeForApprovedUserAndReturnView()
        {
            var user = MockedData.GetApprovedUser();

            MyController<PayslipController>
                .Instance()
                .WithData(d => d.WithSet<User>(u => u.Add(user)))
                .WithUser(u => u.WithIdentifier(user.Id))
                .Calling(c => c.Mine())
                .ShouldReturn()
                .View();
        }

        [Fact]
            public void MineShouldReturnUnauthorizedForUnApprovedUser()
                => MyController<PayslipController>
                    .Instance()
                    .WithUser()
                    .Calling(c => c.Mine())
                    .ShouldReturn()
                    .Unauthorized();

    }
}
