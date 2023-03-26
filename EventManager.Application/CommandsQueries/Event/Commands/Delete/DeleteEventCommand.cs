using MediatR;

namespace EventManager.Application.CommandsQueries.Event.Commands.Delete;

public class DeleteEventCommand : IRequest<Unit>
{
    public Guid EventId { get; set; }
}