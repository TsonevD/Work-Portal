using System;

namespace WorkPortal.Areas.Admin.Models.Employee
{
    public class AllEmployeeViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool IsApproved { get; set; }

    }
}
