using System.Collections.Generic;
using WorkPortal.Models.Shifts;

namespace WorkPortal.Services.Shifts.Models
{
    public class ShiftAssignModel
    {
        public ShiftViewModel Shift { get; set; }

        public int EmployeeId { get; set; }

        public IEnumerable<EmployeeServiceModel> Employees { get; set; }
    }
}
