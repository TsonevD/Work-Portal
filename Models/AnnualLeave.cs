using System;
using System.ComponentModel.DataAnnotations;
using GlobalConstants;
using Models.Enums;

namespace Models
{
    using static DataConstants;
    public class AnnualLeave
    {
        public int Id { get; init; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [MaxLength(DataConstants.AnnualLeave.DaysMaxValue)]
        public int? DaysToBeTaken { get; set; }

        [MaxLength(DataConstants.AnnualLeave.DaysMaxValue)]
        public int? RemainingDays { get; set; }
        
        [Required]
        public string Reason { get; set; }

        public AnnualLeaveStatus Status { get; set; }

        public AnnualLeaveType Type { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}