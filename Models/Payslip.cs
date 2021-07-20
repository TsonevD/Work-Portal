using GlobalConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    using static DataConstants;
    public class Payslip
    {
        public int Id { get; init; }

        [Column(TypeName = DefaultDecimalValue)]
        public decimal RatePerHour { get; set; }

        [Column(TypeName = DefaultDecimalValue)]
        public decimal WorkingHourPerMonth { get; set; }

        public DateTime IssuedOn { get; init; }

        [Column(TypeName = DefaultDecimalValue)]
        public decimal Salary { get; set; }

        public ICollection<Shift> Shifts { get; init; } = new HashSet<Shift>();

        public ICollection<AnnualLeave> AnnualLeaves { get; init; } = new HashSet<AnnualLeave>();

        public ICollection<Employee> Employees { get; init; } = new HashSet<Employee>();


    }
}