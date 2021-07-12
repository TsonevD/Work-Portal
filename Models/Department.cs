using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Department
    {
        public int Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string ManagerId { get; init; }
        public Employee Manager { get; set; }
        public int CompanyId { get; init; }
        public Company Company { get; init; }
        public ICollection<Employee> Employees { get; init; } = new HashSet<Employee>();
    }
}