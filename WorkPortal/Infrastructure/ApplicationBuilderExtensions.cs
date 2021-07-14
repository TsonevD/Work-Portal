using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Models.Enums;
using WorkPortal.Data;

namespace WorkPortal.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<WorkPortalDbContext>();

            data.Database.Migrate();

            SeedCompanyAndDepartments(data);
            SeedEmployees(data);
            SeedShifts(data);

            return app;
        }

        private static void SeedShifts(WorkPortalDbContext data)
        {
            if (data.Shifts.Any())
            {
                return;
            }
            data.Shifts.AddRange(new []
            {
                new Shift()
                {
                    
                }
            });

        }

        private static void SeedEmployees(WorkPortalDbContext? data)
        {
            if (data.Employees.Any())
            {
                return;
            }
            data.Employees.AddRange(new []
            {
                new Employee()
                {
                    FirstName = "Peter",
                    LastName = "Parker",
                    Gender = Gender.Male,
                    HireDate = DateTime.UtcNow,
                    Email = "peter@abv.bg",
                    DepartmentId = 1,
                    JobTitle = "SEO",
                    Phone = 0888123456,
                    Address = new Address()
                    {
                        StreetName = "First Avenue 12",
                        PostCode = "W12",
                        Town = new Town()
                        {
                            Name = "London",
                        },
                    },
                    Shift = new Shift()
                    {
                        HoursWorking = 8,
                        Location = "7th Floor of the Office in London",
                        RatePerHour = 20000,
                        ShiftDate = DateTime.Parse("12/08/2021"),
                        StartTime = TimeSpan.Parse("10:00"),
                        FinishTime = TimeSpan.Parse("18:30"),
                    }
                },
                new Employee()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Gender = Gender.Male,
                    HireDate = DateTime.UtcNow,
                    Email = "ivan@abv.bg",
                    DepartmentId = 4,
                    JobTitle = "VP Finance",
                    Phone = 0899123456,
                    Address = new Address()
                    {
                        StreetName = "Second Avenue 23",
                        PostCode = "W12",
                        Town = new Town()
                        {
                            Name = "London",
                        },
                    },
                    ManagerId = 1,
                    Shifts = new List<Shift>()
                    {
                        new Shift()
                        {
                            HoursWorking = 8,
                            Location = "6th floor in the Main Office in London",
                            RatePerHour = 6000,
                            ShiftDate = DateTime.Parse("12/08/2021"),
                            StartTime = TimeSpan.Parse("10:00"),
                            FinishTime = TimeSpan.Parse("18:30"),
                        },     
                        new Shift()
                        {
                            HoursWorking = 8,
                            Location = "6th floor in the Main Office in London",
                            RatePerHour = 6000,
                            ShiftDate = DateTime.Parse("13/08/2021"),
                            StartTime = TimeSpan.Parse("09:00"),
                            FinishTime = TimeSpan.Parse("17:30"),
                        },
                    }
               
                },
                new Employee()
                {
                    FirstName = "Dim",
                    LastName = "Dim",
                    Gender = Gender.Male,
                    HireDate = DateTime.UtcNow,
                    Email = "dim@abv.bg",
                    DepartmentId = 3,
                    JobTitle = "VP Legal",
                    Phone = 0877453456,
                    Address = new Address()
                    {
                        StreetName = "Third Avenue 23",
                        PostCode = "W12",
                        Town = new Town()
                        {
                            Name = "London",
                        },
                    },
                    ManagerId = 1,
                    Shifts = new List<Shift>()
                    {
                        new Shift()
                        {
                            HoursWorking = 8,
                            Location = "5th floor in the Main Office in London",
                            RatePerHour = 5000,
                            ShiftDate = DateTime.Parse("12/08/2021"),
                            StartTime = TimeSpan.Parse("10:00"),
                            FinishTime = TimeSpan.Parse("18:30"),
                        },
                        new Shift()
                        {
                            HoursWorking = 8,
                            Location = "5th floor in the Main Office in London",
                            RatePerHour = 5000,
                            ShiftDate = DateTime.Parse("13/08/2021"),
                            StartTime = TimeSpan.Parse("09:00"),
                            FinishTime = TimeSpan.Parse("17:30"),
                        },
                    }
                },
                new Employee()
                {
                    FirstName = "Jenna",
                    LastName = "Jonas",
                    Gender = Gender.Female,
                    HireDate = DateTime.UtcNow,
                    Email = "jenna@abv.bg",
                    DepartmentId = 2,
                    JobTitle = "VP HR",
                    Phone = 0877453456,
                    Address = new Address()
                    {
                        StreetName = "Third Avenue 23",
                        PostCode = "W12",
                        Town = new Town()
                        {
                            Name = "London",
                        },
                    },
                    ManagerId = 1,
                    Shifts = new List<Shift>()
                    {
                        new Shift()
                        {
                            HoursWorking = 8,
                            Location = "At Home",
                            RatePerHour = 8000,
                            ShiftDate = DateTime.Parse("12/08/2021"),
                            StartTime = TimeSpan.Parse("07:00"),
                            FinishTime = TimeSpan.Parse("15:30"),
                        },
                        new Shift()
                        {
                            HoursWorking = 8,
                            Location = "At Home",
                            RatePerHour = 8000,
                            ShiftDate = DateTime.Parse("13/08/2021"),
                            StartTime = TimeSpan.Parse("07:00"),
                            FinishTime = TimeSpan.Parse("15:30"),
                        },
                    }
                },
            });
            data.SaveChanges();
        }

        private static void SeedCompanyAndDepartments(WorkPortalDbContext data)
        {
            if (data.Companies.Any() || data.Departments.Any())
            {
                return;
            }
            var company = new Company()
            {
                Name = "WWP Limited",
                Address = "First Avenue 12",
                Bulstat = "12345",
                Town = "New York"
            };
            data.Companies.Add(company);
            data.Departments.AddRange(new []
            {
                new Department()
                {
                    Name = "Executive",
                    Company = company,
                },
                new Department()
                {
                    Company = company,
                    Name = "HR"
                },
                new Department()
                {
                    Company = company,
                    Name = "Legal"
                },
                new Department()
                {
                Company = company,
                Name = "Finance"
                },
                new Department()
                {
                    Company = company,
                    Name = "IT"
                },
            });

            data.SaveChanges();

        }
    }
}
