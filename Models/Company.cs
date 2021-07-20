using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GlobalConstants;

namespace Models
{
    using static DataConstants;
    public class Company
    {
        public int Id { get; init; }
        [Required]
        [StringLength(DefaultNameMaxLength)]
        public string Name { get; init; }
        [Required]
        public string Bulstat { get; init; }
        [Required]
        [StringLength(AddressNameMaxLength)]
        public string Address { get; set; }
        [Required]
        [StringLength(TownNameMaxLength)]
        public string Town { get; set; }
        public ICollection<Department> Departments { get; init; } = new HashSet<Department>();
    }
}