using AutoMapper;
using Models;
using WorkPortal.Models.AnnualLeave;
using WorkPortal.Services.AnnualLeave.Models;
using WorkPortal.Services.Employees.Models;

namespace WorkPortal.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<AnnualLeaveDetailsServiceModel, AnnualLeaveInputModel>();
            this.CreateMap<AnnualLeave, AnnualLeaveDetailsServiceModel>();

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
                    cfg=>cfg.MapFrom(x=>x.Address.Town.Name))
                ;
      

        }
    }
}
