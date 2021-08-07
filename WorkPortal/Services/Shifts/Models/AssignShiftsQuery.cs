using System.Collections.Generic;

namespace WorkPortal.Services.Shifts.Models
{
    public class AssignShiftsQuery
    {
        public ShiftQueryModel Shift { get; set; }

        public ICollection<EmployeeServiceModel> Employee { get; set; }
    }
}
