using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Shift
    {
        public int Id { get; init; }
        public DateTime ShiftDate { get; init; }
        public TimeSpan StartTime { get; init; }
        public TimeSpan FinishTime { get; init; }
        public decimal HoursWorking { get; init; }
        public decimal Rate { get; init; }
        [Required]
        public string Location { get; init; }
        public int PayslipId { get; init; }
        public Payslip Payslip { get; init; }
        public int AnnualLeaveId { get; init; }
        public AnnualLeave AnnualLeave { get; init; }
        public ICollection<AnnualLeave> AnnualLeaves { get; init; } = new HashSet<AnnualLeave>();
        public ICollection<Payslip> Payslips { get; init; } = new HashSet<Payslip>();
    }
}