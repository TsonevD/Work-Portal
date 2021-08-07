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
        [RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = "Time must be between 00:00 to 23:59")]
        public TimeSpan StartTime { get; set; }
        
        [Required]
        [Display(Name = "Finishing Time ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        [RegularExpression(@"((([0-1][0-9])|(2[0-3]))(:[0-5][0-9])(:[0-5][0-9])?)", ErrorMessage = "Time must be between 00:00 to 23:59")]
        public TimeSpan FinishTime { get; set; }

        [Display(Name = "Working hours")]
        [Range(ShiftMinHours , ShiftMaxHours)]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,100})$")]
        public decimal HoursWorking { get; set; }

        [Display(Name = "Rate per hour")]
        [Range(RatePerHourMinValue, RatePerHourMaxValue)]
        [RegularExpression(@"^[0-9]+(\.[0-9]{1,100})$")]
        public decimal RatePerHour { get; set; }
        
        [Required]
        public LocationInputServiceModel Location { get; set; }
    }
}
