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
            data.Shifts.AddRange(new[]
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
            data.Employees.AddRange(new[]
            {
                new Employee()
                {
                    FirstName = "Peter",
                    LastName = "Parker",
                    Gender = Gender.Male,
                    HireDate = DateTime.Parse("17/05/2020"),
                    Email = "peter@abv.bg",
                    DepartmentId = 1,
                    JobTitle = "Catering Manager",
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
                    Shifts = new List<Shift>()
                    {
                        new Shift()
                    {
                        HoursWorking = 8,
                        RatePerHour = 27.80m,
                        ShiftDate = DateTime.Parse("12/08/2021"),
                        StartTime = TimeSpan.Parse("10:00"),
                        FinishTime = TimeSpan.Parse("18:30"),
                        Location = new Location()
                        {
                            PostCode = "2300",
                            StreetName = "St John ",
                            StreetNumber = 26,
                            Town = "London"
                        }
                    }
                        }
                },
                new Employee()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Gender = Gender.Male,
                    HireDate = DateTime.Parse("17/07/2020"),
                    Email = "ivan@abv.bg",
                    DepartmentId = 4,
                    JobTitle = "Chef",
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
                            RatePerHour = 19.25m,
                            ShiftDate = DateTime.Parse("12/08/2021"),
                            StartTime = TimeSpan.Parse("10:00"),
                            FinishTime = TimeSpan.Parse("18:30"),
                            Location = new Location()
                            {
                                PostCode = "2300",
                                StreetName = "St John ",
                                StreetNumber = 26,
                                Town = "London"
                            }
                        },
                        new Shift()
                        {
                            HoursWorking = 8,
                            RatePerHour = 13.25m,
                            ShiftDate = DateTime.Parse("13/08/2021"),
                            StartTime = TimeSpan.Parse("09:00"),
                            FinishTime = TimeSpan.Parse("17:30"),
                            Location = new Location()
                            {
                                PostCode = "2300",
                                StreetName = "St John ",
                                StreetNumber = 26,
                                Town = "London"
                            },
                        },
                    }

                },
                new Employee()
                {
                    FirstName = "Dim",
                    LastName = "Dim",
                    Gender = Gender.Male,
                    HireDate = DateTime.Parse("11/07/2020"),
                    Email = "dim@abv.bg",
                    DepartmentId = 3,
                    JobTitle = "Bartender",
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
                            RatePerHour = 17.50m,
                            ShiftDate = DateTime.Parse("12/08/2021"),
                            StartTime = TimeSpan.Parse("10:00"),
                            FinishTime = TimeSpan.Parse("18:30"),
                            Location = new Location()
                            {
                                PostCode = "2300",
                                StreetName = "St John ",
                                StreetNumber = 26,
                                Town = "London"
                            },
                        },
                        new Shift()
                        {
                            HoursWorking = 8,
                            RatePerHour = 17.50m,
                            ShiftDate = DateTime.Parse("13/08/2021"),
                            StartTime = TimeSpan.Parse("09:00"),
                            FinishTime = TimeSpan.Parse("17:30"),
                            Location = new Location()
                            {
                                PostCode = "2300",
                                StreetName = "St John ",
                                StreetNumber = 26,
                                Town = "London"
                            },
                        },
                    }
                },
                new Employee()
                {
                    FirstName = "Jenna",
                    LastName = "Jonas",
                    Gender = Gender.Female,
                    HireDate = DateTime.Parse("01/07/2020"),
                    Email = "jenna@abv.bg",
                    DepartmentId = 2,
                    JobTitle = "Hostess",
                    Phone = 087745345,
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
                            RatePerHour = 20.25m,
                            ShiftDate = DateTime.Parse("12/08/2021"),
                            StartTime = TimeSpan.Parse("07:00"),
                            FinishTime = TimeSpan.Parse("15:30"),
                            Location = new Location()
                            {
                                PostCode = "2300",
                                StreetName = "St John ",
                                StreetNumber = 26,
                                Town = "London"
                            },

                        },
                        new Shift()
                        {
                            HoursWorking = 8,
                            RatePerHour = 20.25m,
                            ShiftDate = DateTime.Parse("13/08/2021"),
                            StartTime = TimeSpan.Parse("07:00"),
                            FinishTime = TimeSpan.Parse("15:30"),
                            Location = new Location()
                            {
                                PostCode = "2300",
                                StreetName = "St John ",
                                StreetNumber = 26,
                                Town = "London"
                            },
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
            data.Departments.AddRange(new[]
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
