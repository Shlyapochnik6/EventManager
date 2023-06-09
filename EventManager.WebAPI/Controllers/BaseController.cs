using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.WebAPI.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore
    .Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
public class BaseController : ControllerBase
{
    protected readonly IMapper Mapper;
    protected readonly IMediator Mediator;

    public BaseController(IMapper mapper, IMediator mediator)
    {
        Mapper = mapper;
        Mediator = mediator;
    }

    protected Guid UserId => User.Identity!.IsAuthenticated
        ? Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
        : Guid.Empty;
    protected string Role => User.FindFirstValue(ClaimTypes.Role)!;
}