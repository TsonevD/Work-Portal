using System;
using System.Collections.Generic;

namespace Models
{
    public class Payslip
    {
        public int Id { get; init; }

        public decimal RatePerHour { get; init; }

        public decimal WorkingHourPerMonth { get; init; }

        public DateTime IssuedOn { get; init; }

        public decimal Salary { get; init; }

        public ICollection<Shift> Shifts { get; init; } = new HashSet<Shift>();
    }
}