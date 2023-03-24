using System.Net;
using EventManager.Application.Common.Exceptions;
using EventManager.Identity;
using EventManager.Identity.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventManager.Application.CommandsQueries.User.Queries.Login;

public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, AuthenticatedResponse>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public UserLoginQueryHandler(IJwtGenerator jwtGenerator, 
        UserManager<Domain.User> userManager, SignInManager<Domain.User> signInManager)
    {
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<AuthenticatedResponse> Handle(UserLoginQuery request,
        CancellationToken cancellationToken)
    {
        var user = await GetRegisteredUser(request);
        var result = await _signInManager
            .CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
            return await UpdateUserToken(user);
        throw new Exception(HttpStatusCode.Unauthorized.ToString());
    }
    
    private async Task<Domain.User> GetRegisteredUser(UserLoginQuery request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new NotFoundException(request.Email);
        return user;
    }
    
    private async Task<AuthenticatedResponse> UpdateUserToken(Domain.User user)
    {
        var newToken = _jwtGenerator.CreateToken(user);
        var newRefreshToken = _jwtGenerator.CreateRefreshToken();
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);
        return new AuthenticatedResponse(newToken, newRefreshToken);
    }
}