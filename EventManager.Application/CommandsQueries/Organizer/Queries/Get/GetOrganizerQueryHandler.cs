using EventManager.Application.Common.Exceptions;
using EventManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Application.CommandsQueries.Organizer.Queries.Get;

public class GetOrganizerQueryHandler : IRequestHandler<GetOrganizerQuery, Domain.Organizer>
{
    private readonly IEventManagerDbContext _dbContext;

    public GetOrganizerQueryHandler(IEventManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.Organizer> Handle(GetOrganizerQuery request, CancellationToken cancellationToken)
    {
        var organizer = await _dbContext.Organizers
            .FirstOrDefaultAsync(o => o.OrganizerName == request.OrganizerName, cancellationToken);
        if (organizer is null)
            throw new NotFoundException(request.OrganizerName);
        return organizer;
    }
}