using Models;
using Models.Enums;
using MyTested.AspNetCore.Mvc;
using System;

namespace WorkPortal.Tests.Data
{
    public static class MockedData
    {
        public const string USER_ID = "user123";
        public const int EMPLOYEE_ID = 1;
        public const string START_DATE = "14/09/2021";
        public const string END_DATE = "20/09/2021";
        public const string REASON = "Dentist App";
        public const int DAYS = 3;


        public static User GetApprovedUser()
        {
            var user = new User()
            {
                Id = USER_ID,
                FirstName = TestUser.Username,
                LastName = TestUser.Username,
                Email = "test@abv.bg",
                PasswordHash = TestUser.AuthenticationType,
                IsApproved = true,
            };
            return user;

        }
        public static Employee GetEmployee()
        {
            return new Employee()
            {
                Id = EMPLOYEE_ID,
                UserId = USER_ID,
            };
        }
        public static AnnualLeave GetAnnualLeave()
        {
            return new AnnualLeave()
            {
                Id = 1,
                Type = AnnualLeaveType.PaidLeave,
                StartDate = DateTime.Parse(START_DATE),
                EndDate = DateTime.Parse(END_DATE),
                DaysToBeTaken = DAYS,
                Reason = REASON,
                EmployeeId = EMPLOYEE_ID,
            };
        }
        public static Shift GetShift()
        {
            return new Shift()
            {
                Id = 1,
                EmployeeId = EMPLOYEE_ID,
                IsAssigned = true,
                HoursWorking = 5,
                ShiftDate = DateTime.Parse(START_DATE),
                StartTime = TimeSpan.Parse("10:00"),
                FinishTime = TimeSpan.Parse("18:00"),
                RatePerHour = 14.50m,
                Location = new Location()
                {
                    CompanyName = "H2B",
                    PostCode = "W1W",
                    StreetName = "Avenue Street",
                    StreetNumber = 123,
                    Town = "New York"
                },
            };
        }

    }
}
