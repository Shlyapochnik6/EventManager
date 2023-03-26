using AutoMapper;
using EventManager.Application.CommandsQueries.Organizer.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.WebAPI.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore
    .Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[Route("api/organizer")]
public class OrganizerController : BaseController
{
    public OrganizerController(IMapper mapper, IMediator mediator) : 
        base(mapper, mediator) { }
    
    [HttpPost]
    public async Task<ActionResult> Create(string organizerName)
    {
        var command = new CreateOrganizerCommand()
        {
            OrganizerName = organizerName
        };
        await Mediator.Send(command);
        return Ok();
    }
}