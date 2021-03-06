using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GlobalConstants;

namespace Models
{
    using static DataConstants;
    public class Address
    {
        public int Id { get; init; }

        [Required]
        [StringLength(DataConstants.Address.AddressNameMaxLength)]
        public string StreetName { get; set; }

        [Required]
        [StringLength(DataConstants.Address.AddressPostCodeMaxLength)]
        public string PostCode { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public ICollection<Employee> Employees { get; init; } = new HashSet<Employee>();
    }
}