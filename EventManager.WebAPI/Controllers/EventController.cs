using AutoMapper;
using EventManager.Application.CommandsQueries.Event.Commands.Create;
using EventManager.Application.CommandsQueries.Event.Commands.Delete;
using EventManager.Application.CommandsQueries.Event.Commands.Update;
using EventManager.Application.CommandsQueries.Event.Queries.Dtos;
using EventManager.Application.CommandsQueries.Event.Queries.GetAll;
using EventManager.Application.CommandsQueries.Event.Queries.GetDto;
using EventManager.WebAPI.Models.Event;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.WebAPI.Controllers;

[ApiController]
[Route("api/event")]
public class EventController : BaseController
{
    public EventController(IMapper mapper, IMediator mediator) : 
        base(mapper, mediator) { }
    
    [AllowAnonymous]
    [HttpGet("get-by-id/{eventId:guid}")]
    public async Task<ActionResult<GetEventDto>> Get(Guid eventId)
    {
        var query = new GetEventDtoQuery(eventId);
        var eventDto = await Mediator.Send(query);
        return Ok(eventDto);
    }
    
    [AllowAnonymous]
    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<GetEventDto>>> GetAll()
    {
        var query = new GetAllEventsQuery();
        var eventsDtos = await Mediator.Send(query);
        return Ok(eventsDtos);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm] CreateEventDto dto)
    {
        var command = Mapper.Map<CreateEventCommand>(dto);
        var eventId = await Mediator.Send(command);
        return Ok(eventId);
    }
    
    [HttpPut]
    public async Task<ActionResult> Update([FromForm] UpdateEventDto dto)
    {
        var command = Mapper.Map<UpdateEventCommand>(dto);
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpDelete("{eventId:guid}")]
    public async Task<ActionResult> Delete(Guid eventId)
    {
        var command = new DeleteEventCommand(eventId);
        await Mediator.Send(command);
        return Ok();
    }
}