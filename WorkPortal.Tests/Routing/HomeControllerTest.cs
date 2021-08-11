using MyTested.AspNetCore.Mvc;
using WorkPortal.Controllers;
using Xunit;
namespace WorkPortal.Tests.Routing
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
    => MyRouting
        .Configuration()
        .ShouldMap("/")
        .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(c => c.Error());
    }
}
