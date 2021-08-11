using MyTested.AspNetCore.Mvc;
using WorkPortal.Controllers;
using WorkPortal.Services.Employees.Models;
using Xunit;

namespace WorkPortal.Tests.Routing
{
    public class EmployeeControllerTest
    {
        [Fact]
        public void GetProfileShouldBeMapped()
         => MyRouting
             .Configuration()
             .ShouldMap("/Employee/Profile")
             .To<EmployeeController>(c => c.Profile());

        [Fact]
        public void GetCompleteProfileShouldBeMapped()
                          => MyRouting
                       .Configuration()
              .ShouldMap("/Employee/CompleteProfile")
              .To<EmployeeController>(c => c.CompleteProfile());

        [Fact]
        public void PostCompleteProfileShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Employee/CompleteProfile")
                    .WithMethod(HttpMethod.Post))
                .To<EmployeeController>(c => c.CompleteProfile(With.Any<ProfileServiceModel>()));
    }
}
