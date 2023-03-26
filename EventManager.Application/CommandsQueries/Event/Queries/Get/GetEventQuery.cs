using MediatR;

namespace EventManager.Application.CommandsQueries.Event.Queries.Get;

public class GetEventQuery : IRequest<Domain.Event>
{
    public Guid EventId { get; set; }

    public GetEventQuery(Guid eventId)
    {
        EventId = eventId;
    }
}