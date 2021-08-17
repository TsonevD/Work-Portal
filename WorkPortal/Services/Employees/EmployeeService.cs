using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender emailSender;

        public EmployeeService(WorkPortalDbContext data, IConfigurationProvider mapper,
            UserManager<User> userManager, IEmailSender emailSender)
        {
            this.data = data;
            this.mapper = mapper;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public async Task AdminAddUser(AddEmployeeInputModel employee)
        {
            var pass = Guid.NewGuid().ToString("d")
                .Substring(1, 8);
            var user = new User
            {
                UserName = employee.Email,
                Email = employee.Email,
                FirstName = employee.Firstname,
                LastName = employee.Lastname,
                DateOfBirth = employee.DateOfBirth,
                IsApproved = true,
            };

            await this.userManager.CreateAsync(user, pass);

            var newEmployee = new Employee()
            {
                MiddleName = employee.MiddleName,
                Gender = employee.Gender,
                HireDate = employee.HireDate,
                JobTitle = employee.JobTitle,
                ManagerId = employee.ManagerId,
                DepartmentId = employee.DepartmentId,
                Phone = employee.Phone,
                ProfilePictureUrl = employee.ProfilePictureUrl,
                UserId = user.Id,
                User = user,
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

            var subject = "New account created";
            var msg = $"Your account has been created on WorkPortal." +
                      $"Below are your system generated credentials," +
                      $"please change the password immediately after login" +
                      $"Username: {user.Email} Password: {pass}";

            await emailSender.SendEmailAsync(user.Email, subject, msg);
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

        public void CompleteProfile(ProfileServiceModel profile, string userId)
        {
            var user = this.data.Users.Find(userId);

            var employee = new Employee()
            {
                UserId = userId,
                User = user,
                MiddleName = profile.MiddleName,
                Gender = profile.Gender,
                HireDate = profile.HireDate,
                JobTitle = profile.JobTitle,
                ManagerId = profile.ManagerId,
                DepartmentId = profile.DepartmentId,
                Phone = profile.Phone,
                ProfilePictureUrl = profile.ProfilePictureUrl,
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

        public int UserId(string userId)
            => this.data
                .Employees
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

        public User FindUser(string id)
           => this.data.Users.FirstOrDefault(x => x.Id == id);

        public bool IsUserApproved(string userId)
            => this.data.Users.Where(x => x.Id == userId)
                .Any(x => x.IsApproved);

        public async Task AdminApproveUser(User user)
        {
            user.IsApproved = true;
            await this.data.SaveChangesAsync();

            var subject = "Account approval";
            var msg = $"Your account has been approved. You can now enjoy all of the sites functionality." ;

            await emailSender.SendEmailAsync(user.Email, subject, msg);
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
