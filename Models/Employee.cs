using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GlobalConstants;
using Models.Enums;

namespace Models
{
    using static DataConstants;
    public class Employee
    {
        public int Id { get; init; }
        [Required]
        [StringLength(DefaultNameMaxLength)]
        public string FirstName { get; init; }

        [StringLength(DefaultNameMaxLength)]
        public string MiddleName { get; init; }

        [Required]
        [StringLength(DefaultNameMaxLength)]
        public string LastName { get; init; }

        [Required]
        [StringLength(DefaultNameMaxLength)]
        public string JobTitle { get; set; }

        public Gender Gender { get; init; }

        [Range(minimum:PhoneMinLength , maximum: PhoneMinLength)]
        public int Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int? ManagerId { get; set; }

        public Employee Manager { get; set; }

        public DateTime HireDate { get; init; }

        [Url]
        public string ProfilePictureUrl { get; set; }

        public int? AddressId { get; set; }

        public Address Address { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public bool IsDeleted { get; init; }

        public DateTime? DeletedOn { get; init; }

        public ICollection<Employee> InverseManager { get; set; }

        public ICollection<Shift> Shifts { get; init; } = new HashSet<Shift>();

        public ICollection<Payslip> Payslips { get; init; } = new HashSet<Payslip>();

    }
}
