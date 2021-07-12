using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Address
    {
        public int Id { get; init; }
        [Required]
        public string StreetName { get; init; }
        [Required]
        public string PostCode { get; init; }
        [Required]
        public string Town { get; init; }
        public ICollection<Employee> Employees { get; init; } = new HashSet<Employee>();
    }
}