using AutoMapper;
using EventManager.Application.CommandsQueries.User.Commands.RefreshToken;
using EventManager.Application.CommandsQueries.User.Commands.Register;
using EventManager.Application.CommandsQueries.User.Queries.Login;
using EventManager.Identity;
using EventManager.WebAPI.Models.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.WebAPI.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore
    .Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[Route("api/user")]
public class UserController : BaseController
{
    public UserController(IMapper mapper, IMediator mediator) :
        base(mapper, mediator) { }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Register(
        [FromBody] UserRegisterDto dto)
    {
        var command = Mapper.Map<RegisterUserCommand>(dto);
        await Mediator.Send(command);
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticatedResponse>> Login(
        [FromBody] UserLoginDto dto)
    {
        var query = Mapper.Map<LoginUserQuery>(dto);
        var response = await Mediator.Send(query);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthenticatedResponse>> RefreshToken(
        [FromBody] RefreshTokenCommand command)
    {
        var response = await Mediator.Send(command);
        return Ok(response);
    }
}