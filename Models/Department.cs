using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GlobalConstants;

namespace Models
{
    using static DataConstants;
    public class Department
    {
        public int Id { get; init; }
        [Required]
        [StringLength(DefaultNameMaxLength)]
        public string Name { get; init; }
        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<Employee> Employees { get; init; } = new HashSet<Employee>();
    }
}