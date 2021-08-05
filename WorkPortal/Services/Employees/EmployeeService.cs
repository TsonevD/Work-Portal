using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Models;
using WorkPortal.Areas.Admin.Models.Employee;
using WorkPortal.Data;
using WorkPortal.Services.Employees.Models;

namespace WorkPortal.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly WorkPortalDbContext data;
        private readonly UserManager<User> userManager;
        private readonly IConfigurationProvider mapper;

        public EmployeeService(WorkPortalDbContext data, IConfigurationProvider mapper, UserManager<User> userManager)
        {
            this.data = data;
            this.mapper = mapper;
            this.userManager = userManager;
        }


        public async Task AdminAddUser(AddEmployeeInputModel employee)
        {
            var pass = "asd123";
            var user = new User
            {
                UserName = employee.Email,
                Email = employee.Email,
                FirstName = employee.Firstname,
                LastName = employee.Lastname,
                DateOfBirth = employee.DateOfBirth,
                IsApproved = true,
            };

            var result = await this.userManager.CreateAsync(user, pass);

            var newEmployee = new Employee()
            {
                MiddleName = employee.MiddleName,
                Gender = employee.Gender,
                HireDate = employee.HireDate,
                JobTitle = employee.JobTitle,
                ManagerId = employee.ManagerId,
                DepartmentId = employee.DepartmentId,
                Phone = employee.Phone,
                ProfilePictureUrl = employee.ImageUrl,
                UserId = user.Id,
                Address = new Address()
                {
                    PostCode = employee.PostCode,
                    StreetName = employee.StreetName,
                    Town = new Town()
                    {
                        Name = employee.TownName,
                    },
                },
            };
            this.data.Employees.Add(newEmployee);

            await this.data.SaveChangesAsync();

        }

        public ICollection<AllEmployeesServiceModel> AllUnApprovedUsers()
        {
            var all = this.data.Users
                .Where(x => x.IsApproved == false)
                .Select(x => new AllEmployeesServiceModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DateOfBirth = x.DateOfBirth,
                    Email = x.Email,
                    IsApproved = x.IsApproved,
                }).ToList();

            return all;
        }

        public void CompleteProfile(ProfileServiceModel profile,string userId)
        {
            var employee = new Employee()
            {
                UserId = userId,
                MiddleName = profile.MiddleName,
                Gender = profile.Gender,
                HireDate = profile.HireDate,
                JobTitle = profile.JobTitle,
                ManagerId = profile.ManagerId,
                DepartmentId = profile.DepartmentId,
                Phone = profile.Phone,
                ProfilePictureUrl = profile.ImageUrl,
                Address = new Address()
                {
                    PostCode = profile.PostCode,
                    StreetName = profile.StreetName,
                    Town = new Town()
                    {
                        Name = profile.TownName,
                    },
                },
            };

            this.data.Employees.Add(employee);

            this.data.SaveChanges();
        }

        public ProfileQueryModel GetProfile(string userId)
        {
            var profile = this.data.Employees.Where(x => x.UserId == userId)
                .ProjectTo<ProfileQueryModel>(mapper)
                .FirstOrDefault();

            return profile;
        }

        public User FindUser(string id)
           => this.data.Users.FirstOrDefault(x => x.Id == id);
        public bool IsUserApproved(string userId)
            => this.data.Users.Where(x => x.Id == userId)
                .Any(x => x.IsApproved);

        public void AdminApproveUser(User user)
        {
            user.IsApproved = true;
            this.data.SaveChanges();
        }

        public void AdminDeleteUser(User user)
        {
            this.data.Users.Remove(user);
            this.data.SaveChanges();
        }
       
        public ICollection<DepartmentServiceModel> GetDepartments()
                => this.data.Departments
                .Select(x => new DepartmentServiceModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

        public ICollection<ManagersServiceModel> GetManagers()
            => this.data.Employees
                .Where(x => x.ManagerId == null)
                    .Select(x => new ManagersServiceModel()
                    {
                        Id = x.Id,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                    })
                    .ToList();
    }
}
