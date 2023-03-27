using AutoMapper;
using EventManager.Application.Common.Constants.Roles;
using EventManager.Application.Common.Exceptions;
using EventManager.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Application.CommandsQueries.User.Commands.Register;

public class UserRegisterCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEventManagerDbContext _dbContext;
    private readonly UserManager<Domain.User> _userManager;

    public UserRegisterCommandHandler(IMapper mapper, IEventManagerDbContext dbContext, 
        UserManager<Domain.User> userManager)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await GetExistingUser(request, cancellationToken);
        if (existingUser) 
            throw new ExistingUserException();
        await RegisterUser(request);
        return Unit.Value;
    }
    
    private async Task<bool> GetExistingUser(RegisterUserCommand request, 
        CancellationToken cancellationToken)
    {
        var existingUser = await _dbContext.Users.AnyAsync(u =>
            u.Email == request.Email || u.UserName == request.UserName, cancellationToken);
        return existingUser;
    }
    
    private async Task RegisterUser(RegisterUserCommand request)
    {
        var user = _mapper.Map<Domain.User>(request);
        await _userManager.CreateAsync(user, request.Password);
        await _userManager.AddToRoleAsync(user, Roles.User);
    }
}