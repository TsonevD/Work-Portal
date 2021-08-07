using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using WorkPortal.Areas.Admin.Models.Employee;
using WorkPortal.Services.Employees.Models;

namespace WorkPortal.Services.Employees
{
    public interface IEmployeeService
    {
        Task AdminAddUser(AddEmployeeInputModel employee);
        void AdminDeleteUser(User user);
        void AdminApproveUser(User user);

        bool IsUserApproved(string userId);

        ICollection<AllEmployeesServiceModel> AllUnApprovedUsers();

        void CompleteProfile(ProfileServiceModel profile , string userId);

        ProfileQueryModel GetProfile(string userId);

        User FindUser(string id);

        int UserId(string userId);

        ICollection<DepartmentServiceModel> GetDepartments();

        ICollection<ManagersServiceModel> GetManagers();
    }
}
