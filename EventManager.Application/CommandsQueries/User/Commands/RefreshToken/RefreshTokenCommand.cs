using EventManager.Identity;
using MediatR;

namespace EventManager.Application.CommandsQueries.User.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthenticatedResponse>
{
    public string? Token { get; set; }
    
    public string? RefreshToken { get; set; }
    
    public string Email { get; set; }

    public RefreshTokenCommand(string? token, string? refreshToken,
        string email)
    {
        Token = token;
        RefreshToken = refreshToken;
        Email = email;
    }
}