using AutoMapper;
using EventManager.Application.CommandsQueries.Speaker.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.WebAPI.Controllers;

[ApiController]
[Route("api/speaker")]
public class SpeakerController : BaseController
{
    public SpeakerController(IMapper mapper, IMediator mediator) : 
        base(mapper, mediator) { }
    
    [HttpPost]
    public async Task<ActionResult> Create(string speakerName)
    {
        var command = new CreateSpeakerCommand(speakerName);
        await Mediator.Send(command);
        return Ok();
    }
}