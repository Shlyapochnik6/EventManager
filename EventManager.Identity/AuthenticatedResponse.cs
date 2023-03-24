namespace EventManager.Identity;

public class AuthenticatedResponse
{
    public string? Token { get; set; }
    
    public string? RefreshToken { get; set; }

    public AuthenticatedResponse(string token,
        string refreshToken)
    {
        Token = token;
        RefreshToken = refreshToken;
    }
}