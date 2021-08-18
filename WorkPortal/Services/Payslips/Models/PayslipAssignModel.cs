using System.Collections.Generic;
using WorkPortal.Services.Shifts.Models;

namespace WorkPortal.Services.Payslips.Models
{
    public class PayslipAssignModel
    {
        public int EmployeeId { get; set; }

        public int MonthId { get; set; }

        public IEnumerable<EmployeeServiceModel> Employees { get; set; }
    }
}
