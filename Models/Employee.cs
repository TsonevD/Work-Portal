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

        [StringLength(DefaultNameMaxLength)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(DefaultNameMaxLength)]
        public string JobTitle { get; set; }

        public Gender Gender { get; init; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [StringLength(PhoneMaxLength)]
        public string Phone { get; set; }

        public int? ManagerId { get; set; }

        public Employee Manager { get; set; }

        public DateTime HireDate { get; set; }

        [Url]
        public string ProfilePictureUrl { get; set; }

        public int? AddressId { get; set; }

        public Address Address { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<Employee> InverseManager { get; set; } = new HashSet<Employee>();

        public ICollection<AnnualLeave> AnnualLeaves { get; set; } = new HashSet<AnnualLeave>();

        public ICollection<Shift> Shifts { get; init; } = new HashSet<Shift>();

        public ICollection<Payslip> Payslips { get; init; } = new HashSet<Payslip>();

    }
}
