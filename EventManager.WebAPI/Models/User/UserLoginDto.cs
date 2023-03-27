using AutoMapper;
using EventManager.Application.CommandsQueries.User.Queries.Login;
using EventManager.Application.Common.Mappings;

namespace EventManager.WebAPI.Models.User;

public class UserLoginDto : IMapWith<LoginUserQuery>
{
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserLoginDto, LoginUserQuery>()
            .ForMember(u => u.Email,
                opt => opt.MapFrom(u => u.Email))
            .ForMember(u => u.Password,
                opt => opt.MapFrom(u => u.Password));
    }
}