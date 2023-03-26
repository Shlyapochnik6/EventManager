using EventManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Application.CommandsQueries.Event.Queries.Get;

public class GetEventQueryHandler : IRequestHandler<GetEventQuery, Domain.Event>
{
    private readonly IEventManagerDbContext _dbContext;

    public GetEventQueryHandler(IEventManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.Event> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var cityEvent = await _dbContext.Events
            .Include(e => e.Organizer)
            .Include(e => e.Location)
            .Include(e => e.Speaker)
            .FirstOrDefaultAsync(e => e.Id == request.EventId, cancellationToken);
        if (cityEvent == null)
            throw new NullReferenceException($"The event with Id = {request.EventId} was not found!");
        return cityEvent;
    }
}