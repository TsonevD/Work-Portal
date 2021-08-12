using MyTested.AspNetCore.Mvc;
using WorkPortal.Controllers;
using WorkPortal.Models.AnnualLeave;
using Xunit;

namespace WorkPortal.Tests.Routing
{
    public class AnnualLeaveControllerTest
    {

        [Fact]
        public void AllShouldBeMapped()
                  => MyRouting
                .Configuration()
                .ShouldMap("/AnnualLeave/All")
                .To<AnnualLeaveController>(c => c.All());

        [Fact]
        public void GetAddShouldBeMapped()
          => MyRouting
        .Configuration()
        .ShouldMap("/AnnualLeave/Add")
        .To<AnnualLeaveController>(c => c.Add());


        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/AnnualLeave/Add")
                    .WithMethod(HttpMethod.Post))
                .To<AnnualLeaveController>(c => c.Add(With.Any<AnnualLeaveInputModel>()));

        [Fact]
        public void GetEditShouldBeMapped()
                  => MyRouting
                .Configuration()
                .ShouldMap("/AnnualLeave/Edit/1")
                .To<AnnualLeaveController>(c => c.Edit(1));

        [Fact]
        public void PostEditShouldBeMapped()
                  => MyRouting
                      .Configuration()
                      .ShouldMap(request => request
                          .WithPath("/AnnualLeave/Edit/1")
                          .WithMethod(HttpMethod.Post))
                      .To<AnnualLeaveController>(c => c.Edit(1, With.Any<AnnualLeaveInputModel>()));

        [Fact]
        public void DeleteShouldBeMapped()
                   => MyRouting
                 .Configuration()
                 .ShouldMap("/AnnualLeave/Delete/1")
                 .To<AnnualLeaveController>(c => c.Delete(1));

    }
}
