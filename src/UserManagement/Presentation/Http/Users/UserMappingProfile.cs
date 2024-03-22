using AutoMapper;
using Octopus.UserManagement.Core.Domain.Users.Models;
using Octopus.UserManagement.Presentation.Http.Users.Models;

namespace Octopus.UserManagement.Presentation.Http.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<TokenModel, SignInResponse>()
            .ForMember(dest => dest.RefreshToken, map => map.MapFrom(src => src.RefreshToken))
            .ForMember(dest => dest.AccessToken, map => map.MapFrom(src => src.AccessToken))
            .ForMember(dest => dest.ExpireIn, map => map.MapFrom(src => src.AccessTokenExpires.ToUnixTimeSeconds()))
            .ForMember(dest => dest.TokenType, map => map.MapFrom(src => src.TokenType))
            .ForMember(dest => dest.PhoneNumber, map => map.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.UserName, map => map.MapFrom(src => src.UserName));
    }
}
