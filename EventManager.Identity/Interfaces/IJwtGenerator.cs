using System.Security.Claims;
using EventManager.Domain;

namespace EventManager.Identity.Interfaces;

public interface IJwtGenerator
{
    string CreateToken(User user);

    string CreateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}