using AutoMapper;
using EventManager.Application.CommandsQueries.User.Commands.Register;
using EventManager.Application.Common.Mappings;

namespace EventManager.WebAPI.Models.User;

public class UserRegisterDto : IMapWith<UserRegisterCommand>
{
    public string UserName { get; set; }
    
    public string Email { get; set; }

    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRegisterDto, UserRegisterCommand>()
            .ForMember(u => u.UserName,
                opt => opt.MapFrom(u => u.UserName))
            .ForMember(u => u.Email,
                opt => opt.MapFrom(u => u.Email));
    }
}