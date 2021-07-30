using System;

namespace WorkPortal.Areas.Admin.Models.Employee
{
    public class AddViewModel
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

    }
}
