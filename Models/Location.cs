using System.ComponentModel.DataAnnotations;
using GlobalConstants;

namespace Models
{
    using static DataConstants;
    public class Location
    {
        public int Id { get; set; }

        [Required]
        [StringLength(AddressNameMaxLength)]
        public string Town { get; set; }

        [Required]
        [StringLength(AddressNameMaxLength)]

        public string StreetName { get; set; }

        public int StreetNumber { get; set; }

        [Required]
        [StringLength(AddressPostCodeMaxLength)]
        public string PostCode { get; set; }
        [Required]
        [StringLength(DefaultNameMaxLength)]
        public string CompanyName { get; set; }
    }
}