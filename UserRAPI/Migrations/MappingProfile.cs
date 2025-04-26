using AutoMapper;
using UserRAPI.DTO;
using UserRAPI.Models;

namespace UserRAPI.Migrations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserforRegistrationDTO, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
