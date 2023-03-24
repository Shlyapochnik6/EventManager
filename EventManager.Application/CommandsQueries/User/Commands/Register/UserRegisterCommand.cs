using AutoMapper;
using EventManager.Application.Common.Mappings;
using EventManager.Identity;
using MediatR;

namespace EventManager.Application.CommandsQueries.User.Commands.Register;

public class UserRegisterCommand : IRequest<Unit>, IMapWith<Domain.User>
{
    public string UserName { get; set; }
    
    public string Email { get; set; }

    public string Password { get; set; }

    public UserRegisterCommand(string userName, string email,
        string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRegisterCommand, Domain.User>()
            .ForMember(u => u.UserName,
                opt => opt.MapFrom(u => u.UserName))
            .ForMember(u => u.Email,
                opt => opt.MapFrom(u => u.Email));
    }
}