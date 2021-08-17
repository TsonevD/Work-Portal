using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GlobalConstants;
using Models.Enums;
using WorkPortal.Services.Employees.Models;

namespace WorkPortal.Areas.Admin.Models.Employee
{
    using static DataConstants;
    public class AddEmployeeInputModel
    {
        [Required]
        [StringLength(DefaultNameMaxLength , MinimumLength = DefaultNameMinLength)]
        public string Firstname { get; set; }

        [StringLength(DefaultNameMaxLength, MinimumLength = DefaultNameMinLength)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(DefaultNameMaxLength, MinimumLength = DefaultNameMinLength)]
        public string Lastname { get; set; }

        [Required]
        [StringLength(PhoneMaxLength , MinimumLength = PhoneMinLength)]
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public Gender Gender { get; set; }
        [Required]
        [Url]
        public string ProfilePictureUrl { get; set; }
        public DateTime HireDate { get; set; }
        [Required]
        [StringLength(JobTitleMaxLength , MinimumLength = JobTitleMinLength)]
        public string JobTitle { get; set; }

        [Required]
        [StringLength(Address.AddressNameMaxLength , MinimumLength = Address.AddressNameMinLength)]
        public string StreetName { get; set; }

        [Required]
        [StringLength(Address.AddressPostCodeMaxLength , MinimumLength = Address.AddressPostCodeMinLength)]
        public string PostCode { get; set; }
        [Required]
        [StringLength(TownNameMaxLength , MinimumLength = TownNameMinLength)]
        public string TownName { get; set; }

        [Display(Name = "Reporting Employee")]
        public int ManagerId { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public IEnumerable<DepartmentServiceModel> Departments { get; set; }

        public IEnumerable<ManagersServiceModel> Managers { get; set; }

    }
}
