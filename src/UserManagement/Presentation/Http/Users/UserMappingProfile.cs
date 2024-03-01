using AutoMapper;
using Octopus.UserManagement.Core.Contract.Users.Models;
using Octopus.UserManagement.Presentation.Http.Users.Models;

namespace Octopus.UserManagement.Presentation.Http.Users;

public class UserMappingProfile : Profile
{
	public UserMappingProfile()
	{
		CreateMap<SignInModel, SignInResponse>();
	}
}
