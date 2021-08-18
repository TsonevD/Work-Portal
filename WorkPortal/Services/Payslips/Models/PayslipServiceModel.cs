namespace WorkPortal.Services.Payslips.Models
{
    public class PayslipServiceModel
    {

        public decimal WorkingHoursPerMonth { get; set; }

        public int Year { get; init; }

        public int Month { get; set; }

        public decimal BeforeTaxSalary { get; set; }

        public decimal AfterTaxSalary { get; set; }

        public decimal Taxes { get; set; }

        public int EmployeeId { get; set; }

    }
}
