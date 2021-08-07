using System.ComponentModel.DataAnnotations;
using GlobalConstants;

namespace Models
{
    using static DataConstants;
    using static DataConstants.Address;
    public class Location
    {
        public int Id { get; set; }

        [Required]
        [StringLength(AddressNameMaxLength)]
        public string Town { get; set; }

        [Required]
        [StringLength(AddressNameMaxLength)]

        public string StreetName { get; set; }
        [MaxLength()]
        public int StreetNumber { get; set; }

        [Required]
        [StringLength(AddressPostCodeMaxLength)]
        public string PostCode { get; set; }
        [Required]
        [StringLength(CompanyMaxLength)]
        public string CompanyName { get; set; }
    }
}