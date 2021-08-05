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


        ICollection<AllEmployeesServiceModel> All();

        void CompleteProfile(ProfileServiceModel profile , string userId);

        ProfileQueryModel GetProfile(string userId);

        User FindUser(string id);

    
        ICollection<DepartmentServiceModel> GetDepartments();

        ICollection<ManagersServiceModel> GetManagers();
    }
}
