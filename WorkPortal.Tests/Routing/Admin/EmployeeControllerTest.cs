using MyTested.AspNetCore.Mvc;
using WorkPortal.Areas.Admin.Controllers;
using Xunit;

namespace WorkPortal.Tests.Routing.Admin
{

    public class EmployeeControllerTest
    {
        [Fact]
        public void GetAllShouldBeMapped()
        => MyRouting
            .Configuration()
            .ShouldMap("Admin/Employee/All")
            .To<EmployeeController>(c => c.All());


        [Fact]
        public void GetAddShouldBeMapped()
         => MyRouting
        .Configuration()
        .ShouldMap("Admin/Employee/Add")
        .To<EmployeeController>(c => c.Add());

    }
}
