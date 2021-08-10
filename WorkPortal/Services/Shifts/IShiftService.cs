using System.Collections.Generic;
using WorkPortal.Services.Shifts.Models;

namespace WorkPortal.Services.Shifts
{
    public interface IShiftService
    {
        void Add(ShiftInputServiceModel shift);

        void Assign(int id , int employeeId);

        ICollection<ShiftQueryModel> All();

        ICollection<EmployeeServiceModel> AllEmployees();

        ICollection<ShiftQueryModel> Mine(int Id);

        ShiftQueryModel Details(int id);


        ShiftQueryModel FindShift(int id);
    }
}
