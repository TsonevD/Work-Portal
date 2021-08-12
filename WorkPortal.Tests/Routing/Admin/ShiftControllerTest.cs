using MyTested.AspNetCore.Mvc;
using WorkPortal.Areas.Admin.Controllers;
using Xunit;

namespace WorkPortal.Tests.Routing.Admin
{
    public class ShiftControllerTest
    {
        [Fact]
        public void AllShouldBeMapped()
                => MyRouting
                    .Configuration()
                    .ShouldMap("Admin/Shift/All")
                    .To<ShiftController>(c => c.All());


        [Fact]
        public void AddShouldBeMapped()
                  => MyRouting
                 .Configuration()
                 .ShouldMap("Admin/Shift/Add")
                 .To<ShiftController>(c => c.Add());

        [Fact]
        public void AssignShouldBeMapped()
                         => MyRouting
                        .Configuration()
                    .ShouldMap("Admin/Shift/Assign/1")
                    .To<ShiftController>(c => c.Assign(1));

    }
}
