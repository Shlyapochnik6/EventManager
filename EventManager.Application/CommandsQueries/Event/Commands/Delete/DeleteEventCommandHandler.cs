using EventManager.Application.CommandsQueries.Event.Queries.Get;
using EventManager.Application.Interfaces;
using MediatR;

namespace EventManager.Application.CommandsQueries.Event.Commands.Delete;

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IEventManagerDbContext _dbContext;

    public DeleteEventCommandHandler(IMediator mediator,
        IEventManagerDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(DeleteEventCommand request,
        CancellationToken cancellationToken)
    {
        var cityEvent = await GetEvent(request.EventId, cancellationToken);
        _dbContext.Events.Remove(cityEvent);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
    
    private async Task<Domain.Event> GetEvent(Guid eventId,
        CancellationToken cancellationToken)
    {
        var query = new GetEventQuery(eventId);
        var cityEvent = await _mediator.Send(query, cancellationToken);
        return cityEvent;
    }
}