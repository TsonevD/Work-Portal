using System.ComponentModel.DataAnnotations;
using GlobalConstants;

namespace WorkPortal.Services.Shifts.Models
{
    using static DataConstants;
    public class LocationInputServiceModel
    {
        [Required]
        [StringLength(TownNameMaxLength , MinimumLength = TownNameMinLength)]
        public string Town { get; set; }
        [Required]
        [StringLength(Address.AddressNameMaxLength ,MinimumLength = Address.AddressNameMinLength)]
        public string StreetName { get; set; }
        [Required]
        [Range(Address.AddressNumberMinValue, Address.AddressNumberMaxValue)]
        public int StreetNumber { get; set; }
        [Required]
        [StringLength(Address.AddressPostCodeMaxLength, MinimumLength = Address.AddressPostCodeMinLength)]
        public string PostCode { get; set; }
        [Required]
        [StringLength(CompanyMaxLength , MinimumLength = CompanyMinLength)]
        public string CompanyName { get; set; }
    }
}
