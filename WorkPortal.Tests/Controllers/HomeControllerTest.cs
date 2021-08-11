using MyTested.AspNetCore.Mvc;
using WorkPortal.Controllers;
using Xunit;

namespace WorkPortal.Tests.Controllers
{

    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();

        [Fact]
        public void IndexShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();
    }
}
