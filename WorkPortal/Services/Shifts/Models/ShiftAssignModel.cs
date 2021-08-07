using System.Collections.Generic;

namespace WorkPortal.Services.Shifts.Models
{
    public class ShiftAssignModel
    {
        public ShiftQueryModel Shift { get; set; }

        public int EmployeeId { get; set; }

        public IEnumerable<EmployeeServiceModel> Employees { get; set; }
    }
}
