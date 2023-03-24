using EventManager.Identity;
using MediatR;

namespace EventManager.Application.CommandsQueries.User.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthenticatedResponse>
{
    public string? Token { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public string Email { get; set; }
}