using EventManager.Application.Common.Exceptions;
using EventManager.Application.Interfaces;
using EventManager.Identity;
using EventManager.Identity.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Application.CommandsQueries.User.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticatedResponse>
{
    private readonly IEventManagerDbContext _dbContext;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<Domain.User> _userManager;

    public RefreshTokenCommandHandler(IJwtGenerator jwtGenerator,
        UserManager<Domain.User> userManager, IEventManagerDbContext dbContext)
    {
        _dbContext = dbContext;
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
    }
    
    public async Task<AuthenticatedResponse> Handle(RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        if (await GetExistingUser(request, cancellationToken)) 
            throw new ExistingUserException();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (ValidateRefreshToken(request, user))
            throw new Exception("Invalid client request");
        return await UpdateUserToken(user);
    }
    
    private async Task<bool> GetExistingUser(RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _dbContext.Users.AnyAsync(u =>
            u.Email == request.Email, cancellationToken);
        return existingUser;
    }

    private bool ValidateRefreshToken(RefreshTokenCommand request, 
        Domain.User user)
    {
        var validToken = user.RefreshToken != request.RefreshToken || 
                         user.RefreshTokenExpiryTime <= DateTime.UtcNow;
        return validToken;
    }

    private async Task<AuthenticatedResponse> UpdateUserToken(Domain.User user)
    {
        var newToken = _jwtGenerator.CreateToken(user);
        var newRefreshToken = _jwtGenerator.CreateRefreshToken();
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);
        return new AuthenticatedResponse(newRefreshToken, newToken);
    }
}