using System.Collections.Generic;
using WorkPortal.Services.AnnualLeaves.Models;
using WorkPortal.Services.Shifts.Models;

namespace WorkPortal.Services.Payslips.Models
{
    public class PayslipQueryModel
    {
        public int EmployeeId { get; set; }

        public int MonthId { get; set; }

        public ICollection<ShiftQueryModel> Shifts { get; set; }

        public ICollection<AnnualLeaveServiceModel> AnnualLeave { get; set; }
    }
}
