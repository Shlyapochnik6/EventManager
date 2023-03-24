using EventManager.Identity;
using MediatR;

namespace EventManager.Application.CommandsQueries.User.Queries.Login;

public class UserLoginQuery : IRequest<AuthenticatedResponse>
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}