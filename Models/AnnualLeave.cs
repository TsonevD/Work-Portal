using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GlobalConstants;
using Models.Enums;

namespace Models
{
    using static DataConstants;
    public class AnnualLeave
    {
        public int Id { get; init; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        [Column(TypeName = DefaultDecimalValue)]
        [Range(minimum:AnnualLeaveMinDaysValue , maximum:AnnualLeaveMaxDaysValue)]
        public decimal? TakenDays { get; set; }

        [Column(TypeName = DefaultDecimalValue)]
        [Range(minimum: AnnualLeaveMinDaysValue, maximum: AnnualLeaveMaxDaysValue)]
        public decimal? RemainingDays { get; set; }

        public AnnualLeaveStatus Status { get; set; }

        public ICollection<Payslip> Payslips { get; init; } = new HashSet<Payslip>();

    }
}