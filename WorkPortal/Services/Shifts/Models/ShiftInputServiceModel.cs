using System;
using System.ComponentModel.DataAnnotations;
using GlobalConstants;

namespace WorkPortal.Services.Shifts.Models
{
    using static DataConstants;
    public class ShiftInputServiceModel
    {
        [Required]
        [Display(Name = "Shift Date")]
        public DateTime ShiftDate { get; set; }

        [Required]
        [Display(Name = "Starting Time ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):(00|30)$", ErrorMessage = "The starting time should be in format 10:00 or 10:30 only.")]
        public TimeSpan StartTime { get; set; }
        
        [Required]
        [Display(Name = "Finishing Time ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):(00|30)$", ErrorMessage = "The finish time should be in format 10:00 or 10:30 only.")]
        public TimeSpan FinishTime { get; set; }

        [Display(Name = "Working hours")]
        [RegularExpression(@"^([0-1]?[0-9]|2[0-3]):(00|30)$" , ErrorMessage = "The hours working should be in format 10:00 or 10:30 only.")]
        [Range(ShiftMinHours , ShiftMaxHours, ErrorMessage = "The rate should be between {1} and {2} $")]
        public decimal HoursWorking { get; set; }

        [Display(Name = "Rate per hour")]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$")]
        [Range(RatePerHourMinValue, RatePerHourMaxValue , ErrorMessage = "The rate should be between {1} and {2} $")]
        public decimal RatePerHour { get; set; }
        
        [Required]
        public LocationInputServiceModel Location { get; set; }
    }
}
