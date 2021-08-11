using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyTested.AspNetCore.Mvc;
using WorkPortal.Services.Employees;
using WorkPortal;
using static Moq.Mocks;
using Models;
using WorkPortal.Data;

namespace WorkPortal.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public void ConfigureTestServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddIdentityCore<User>();

            //services.ReplaceTransient<IEmployeeService>(_ => EmployeeService);
        }
    }
}
