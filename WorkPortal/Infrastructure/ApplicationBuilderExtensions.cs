using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using WorkPortal.Areas.Admin;
using WorkPortal.Data;

namespace WorkPortal.Infrastructure
{
    using static AdminConstants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedAdministrator(services);
            SeedCompanyAndDepartments(services);
            //SeedEmployees(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<WorkPortalDbContext>();

            data.Database.Migrate();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@workportal.com";
                    const string adminPassword = "admin123";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FirstName = AdministratorRoleName,
                        LastName = AdministratorRoleName,
                        DateOfBirth = DateTime.UtcNow,
                        IsApproved = true,
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }



        private static void SeedCompanyAndDepartments(IServiceProvider services)
        {
            var data = services.GetRequiredService<WorkPortalDbContext>();

            if (data.Companies.Any() || data.Departments.Any())
            {
                return;
            }
            var company = new Company()
            {
                Name = "WWP Limited",
                Address = "St Andrews street 12",
                Bulstat = "12345",
                Town = "London"
            };

            data.Companies.Add(company);
            data.Departments.AddRange(new[]
            {
                new Department()
                {
                    Name = "Management",
                    Company = company,
                },
                new Department()
                {
                    Company = company,
                    Name = "Kitchen"
                },
                new Department()
                {
                    Company = company,
                    Name = "Front Office"
                },
                new Department()
                {
                Company = company,
                Name = "Catering"
                },
                new Department()
                {
                    Company = company,
                    Name = "IT"
                },
            });
            data.SaveChanges();
        }
        //private static void SeedEmployees(IServiceProvider services)
        //{
        //    var data = services.GetRequiredService<WorkPortalDbContext>();

        //    if (data.Employees.Any())
        //    {
        //        return;
        //    }
        //    data.Employees.AddRange(new[]
        //    {
        //        new Employee()
        //        {
        //            FirstName = "Peter",
        //            LastName = "Parker",
        //            Gender = Gender.Male,
        //            HireDate = DateTime.Parse("17/05/2020"),
        //            Email = "peter@abv.bg",
        //            DepartmentId = 1,
        //            JobTitle = "Catering Manager",
        //            ProfilePictureUrl = "https://st.depositphotos.com/2101611/4338/v/600/depositphotos_43381243-stock-illustration-male-avatar-profile-picture.jpg",
        //            Phone = "0888123456",
        //            Address = new Address()
        //            {
        //                StreetName = "First Avenue 12",
        //                PostCode = "W12",
        //                Town = new Town()
        //                {
        //                    Name = "London",
        //                },
        //            },
        //        },
        //        new Employee()
        //        {
        //            FirstName = "Ivan",
        //            LastName = "Ivanov",
        //            Gender = Gender.Male,
        //            HireDate = DateTime.Parse("17/07/2020"),
        //            Email = "ivan@abv.bg",
        //            DepartmentId = 2,
        //            ProfilePictureUrl = "https://lh3.googleusercontent.com/proxy/P0DAnWIuO6cMmkm6BcNXSNSDgESlw2_60f6QY2Gfkm5HAnp-rEpt0oOUrXmH_yjN6s3zgkmcSOv3iXotv9jWMXKQoWi_N0d_t9hFg4-OgoNP",
        //            JobTitle = "Chef",
        //            Phone = "0899123456",
        //            Address = new Address()
        //            {
        //                StreetName = "Second Avenue 23",
        //                PostCode = "W12",
        //                Town = new Town()
        //                {
        //                    Name = "London",
        //                },
        //            },
        //            ManagerId = 1,
        //        },
        //        new Employee()
        //        {
        //            FirstName = "Dim",
        //            LastName = "Dim",
        //            Gender = Gender.Male,
        //            HireDate = DateTime.Parse("11/07/2020"),
        //            Email = "dim@abv.bg",
        //            DepartmentId = 4,
        //            JobTitle = "Bartender",
        //            ProfilePictureUrl = "https://icon-library.com/images/no-profile-picture-icon/no-profile-picture-icon-19.jpg",
        //            Phone = "0877453456",
        //            Address = new Address()
        //            {
        //                StreetName = "Third Avenue 23",
        //                PostCode = "W12",
        //                Town = new Town()
        //                {
        //                    Name = "London",
        //                },
        //            },
        //            ManagerId = 1,
        //        },
        //        new Employee()
        //        {
        //            FirstName = "Jenna",
        //            LastName = "Jonas",
        //            Gender = Gender.Female,
        //            HireDate = DateTime.Parse("01/07/2020"),
        //            Email = "jenna@abv.bg",
        //            DepartmentId = 3,
        //            JobTitle = "Hostess",
        //            ProfilePictureUrl = "https://image.shutterstock.com/image-vector/woman-profile-picture-vector-260nw-438752173.jpg",
        //            Phone = "087745345",
        //            Address = new Address()
        //            {
        //                StreetName = "Third Avenue 23",
        //                PostCode = "W12",
        //                Town = new Town()
        //                {
        //                    Name = "London",
        //                },
        //            },
        //            ManagerId = 1,
        //        },
        //    });
        //    data.SaveChanges();
        //}
    }
}
