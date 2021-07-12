using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Company
    {
        public int Id { get; init; }
        [Required]
        public string Name { get; init; }
        [Required]
        public string Bulstat { get; init; }
        [Required]
        public string Address { get; init; }
        [Required]
        public string Town { get; init; }
        public ICollection<Department> Departments { get; init; } = new HashSet<Department>();
    }
}