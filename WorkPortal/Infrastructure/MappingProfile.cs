using AutoMapper;
using Models;
using WorkPortal.Models.AnnualLeave;
using WorkPortal.Services.AnnualLeave.Models;

namespace WorkPortal.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<AnnualLeaveDetailsServiceModel, AnnualLeaveInputModel>();
            this.CreateMap<AnnualLeave, AnnualLeaveDetailsServiceModel>();

        }
    }
}
