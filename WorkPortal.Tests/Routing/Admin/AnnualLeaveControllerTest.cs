using MyTested.AspNetCore.Mvc;
using WorkPortal.Areas.Admin.Controllers;
using Xunit;

namespace WorkPortal.Tests.Routing.Admin
{
    public class AnnualLeaveControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
                => MyRouting
                    .Configuration()
                    .ShouldMap("Admin/AnnualLeave/All")
                    .To<AnnualLeaveController>(c => c.All());


        [Fact]
        public void ApproveShouldBeMapped()
                  => MyRouting
                 .Configuration()
                 .ShouldMap("Admin/AnnualLeave/Approve/1")
                 .To<AnnualLeaveController>(c => c.Approve(1));

        [Fact]
        public void DeclineShouldBeMapped()
                 => MyRouting
                .Configuration()
            .ShouldMap("Admin/AnnualLeave/Decline/2")
            .To<AnnualLeaveController>(c => c.Decline(2));
    }
}
