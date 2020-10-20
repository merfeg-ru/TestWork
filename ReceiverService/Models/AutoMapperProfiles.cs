using AutoMapper;
using CommonData;

namespace Receiver.Models
{
    public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<User, UserDTO>();
			CreateMap<IUser, UserDTO>();

			CreateMap<UserDTO, User>();

			CreateMap<Organization, OrganizationDTO>();
			CreateMap<OrganizationDTO, Organization>();
		}
	}
}
