using EventManager.Application.CommandsQueries.Event.Queries.Dtos;
using MediatR;

namespace EventManager.Application.CommandsQueries.Event.Queries.GetDto;

public class GetEventDtoQuery : IRequest<GetEventDto>
{
    public Guid EventId { get; set; }
}