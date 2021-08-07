using AutoMapper;
using Models;
using WorkPortal.Models.AnnualLeave;
using WorkPortal.Models.Shifts;
using WorkPortal.Services.AnnualLeaves.Models;
using WorkPortal.Services.Employees.Models;
using WorkPortal.Services.Shifts.Models;

namespace WorkPortal.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<AnnualLeaveDetailsServiceModel, AnnualLeaveInputModel>();
            this.CreateMap<AnnualLeave, AnnualLeaveDetailsServiceModel>();

            this.CreateMap<Employee, AllAnnualLeavesServiceModel>();


            this.CreateMap<AnnualLeave, AllAnnualLeavesServiceModel>()
                .ForMember(x=>x.FirstName,
                    cfg=>cfg.MapFrom(x=>x.Employee.User.FirstName))
                .ForMember(x => x.LastName,
                    cfg => cfg.MapFrom(x => x.Employee.User.LastName))
                ;

            this.CreateMap<Employee, ProfileQueryModel>()
                .ForMember(x=>x.DateOfBirth,
                    cfg => cfg.MapFrom(x=>x.User.DateOfBirth))
                .ForMember(x=>x.Email,
                    cfg=>cfg.MapFrom(x=>x.User.Email))
                .ForMember(x=>x.ManagerFirstName,
                    cfg=>cfg.MapFrom(x=>x.Manager.User.FirstName))
                .ForMember(x => x.ManagerLastName,
                    cfg => cfg.MapFrom(x => x.Manager.User.LastName))
                .ForMember(x=>x.TownName,
                    cfg=>cfg.MapFrom(x=>x.Address.Town.Name));

            this.CreateMap<Shift, ShiftAssignModel>();

            this.CreateMap<Shift, ShiftQueryModel>();

            this.CreateMap<Location, LocationQueryModel>();


            this.CreateMap<Employee, EmployeeServiceModel>()
                .ForMember(x=>x.UserFirstName,
                    cfg=>cfg.MapFrom(x=>x.User.FirstName))
                .ForMember(x => x.UserLastName,
                    cfg => cfg.MapFrom(x => x.User.LastName));

        }
    }
}
