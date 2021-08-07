using System.Collections.Generic;
using WorkPortal.Models.Shifts;
using WorkPortal.Services.Shifts.Models;

namespace WorkPortal.Services.Shifts
{
    public interface IShiftService
    {
        void Add(ShiftInputServiceModel shift);

        void Assign(ShiftQueryModel query);

        ICollection<ShiftQueryModel> All();

        ICollection<EmployeeServiceModel> AllEmployees();

        ICollection<ShiftViewModel> Mine(int Id);

        ShiftViewModel Details(int id);


        ShiftViewModel FindShift(int id);


    }
}
