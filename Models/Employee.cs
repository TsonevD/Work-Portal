using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Enums;

namespace Models
{
    public class Employee
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string FirstName { get; init; }
        [Required]
        public string LastName { get; init; }
        [Required]
        public string JobTitle { get; init; }
        public Gender Gender { get; init; }
        public int Phone { get; init; }
        [Required]
        [EmailAddress]
        public string Email { get; init; }
        public string ManagerId { get; init; }
        public Employee Manager { get; init; }
        public DateTime HireDate { get; init; }
        [Url]
        public string ProfilePictureUrl { get; init; }
        public int AddressId { get; init; }
        public Address Address { get; init; }
        public int DepartmentId { get; init; }
        public Department Department { get; init; }
        public int ShiftId { get; init; }
        public Shift Shift { get; init; }
        public bool IsDeleted { get; init; }
        public DateTime? DeletedOn { get; init; }
        public ICollection<Employee> InverseManager { get; set; }
        public ICollection<Shift> Shifts { get; init; } = new HashSet<Shift>();
        public ICollection<Department> Departments { get; init; } = new HashSet<Department>();
    }

}
