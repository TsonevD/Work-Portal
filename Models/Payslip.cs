using GlobalConstants;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    using static DataConstants;
    public class Payslip
    {
        public int Id { get; init; }

        [Column(TypeName = DefaultDecimalValue)]
        public decimal WorkingHoursPerMonth { get; set; }

        public int Year { get; init; }

        [Range(MonthsMinValue,maximum:MonthsMaxValue)]
        public int Month { get; set; }

        [Column(TypeName = DefaultDecimalValue)]
        public decimal BeforeTaxSalary { get; set; }

        [Column(TypeName = DefaultDecimalValue)]
        public decimal AfterTaxSalary { get; set; }

        [Column(TypeName = DefaultDecimalValue)]
        public decimal Taxes { get; set; }

        public int EmployeeId { get; set; }

        public ICollection<Shift> Shifts { get; init; } = new HashSet<Shift>();

        public ICollection<AnnualLeave> AnnualLeaves { get; init; } = new HashSet<AnnualLeave>();
    }
}