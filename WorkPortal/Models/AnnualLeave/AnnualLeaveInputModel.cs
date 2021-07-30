using System;
using System.ComponentModel.DataAnnotations;
using GlobalConstants;
using Models.Enums;

namespace WorkPortal.Models.AnnualLeave
{
    using static DataConstants.AnnualLeave;
    public class AnnualLeaveInputModel
    {
        [Required]
        public AnnualLeaveType LeaveType { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(ReasonMaxLength, MinimumLength = ReasonMinLength,
        ErrorMessage = "The field Reason must be a with a minimum length of {2}.")]
        public string Reason { get; set; }

    }
}
