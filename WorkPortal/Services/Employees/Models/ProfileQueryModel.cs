using System;
using Models.Enums;

namespace WorkPortal.Services.Employees.Models
{
    public class ProfileQueryModel
    {
        public int Id { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }

        public Gender Gender { get; set; }

        public string ProfilePictureUrl { get; set; }

        public DateTime HireDate { get; set; }

        public string JobTitle { get; set; }

        public string DepartmentName { get; set; }

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }


        public string AddressStreetName { get; set; }

        public string AddressPostCode { get; set; }

        public string TownName { get; set; }

    }
}
