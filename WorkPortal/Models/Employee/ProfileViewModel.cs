using System;

namespace WorkPortal.Models.Employee
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public DateTime HireDate { get; set; }

        public string JobTitle { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLocation { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

    }
}
