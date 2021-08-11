using Models;
using MyTested.AspNetCore.Mvc;
using WorkPortal.Controllers;
using Xunit;

namespace WorkPortal.Tests.Controllers
{
    public class AnnualLeaveControllerTest
    {
        //[Fact]
        //public void AnnualLeaveShouldBeForApprovedUserAndReturnView()
        //{
        //    var user = new User()
        //    {
        //        Id = TestUser.Username,
        //        FirstName = TestUser.Username,
        //        LastName = TestUser.Username,
        //        IsApproved = true,
        //    };

        //    //MyMvc
        //    //    .Pipeline()
        //    //    .ShouldMap(request => request
        //    //    .WithUser(user => user.WithUsername("asd123")));



        //    MyController<AnnualLeaveController>
        //        .Instance(controller => controller
        //        .WithData(ApprovedUser())
        //        .WithUser())
        //        .Calling(c=>c.All())
        //    .ShouldHave()
        //    .ActionAttributes(attributes => attributes
        //    .RestrictingForAuthorizedRequests())
        //    .AndAlso()
        //    .ShouldReturn()
        //    .View();
     
        //}



        //private User ApprovedUser()
        //{
        //    var user = new User()
        //    {
        //        IsApproved = true,
        //        FirstName = TestUser.Username,
        //        LastName = TestUser.Username,
        //        Email = "test@abv.bg",
        //        PasswordHash = TestUser.AuthenticationType,
        //    };

        //    return user;
        //}
    }
}
