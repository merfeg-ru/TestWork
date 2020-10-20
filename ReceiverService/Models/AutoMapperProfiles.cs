using AutoMapper;
using CommonData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
