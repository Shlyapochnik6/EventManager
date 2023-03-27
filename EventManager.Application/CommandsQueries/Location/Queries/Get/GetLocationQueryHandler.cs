using EventManager.Application.Common.Exceptions;
using EventManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Application.CommandsQueries.Location.Queries.Get;

public class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, Domain.Location>
{
    private readonly IEventManagerDbContext _dbContext;

    public GetLocationQueryHandler(IEventManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.Location> Handle(GetLocationQuery request,
        CancellationToken cancellationToken)
    {
        var location = await _dbContext.Locations
            .FirstOrDefaultAsync(l => l.CityName == request.CityName, cancellationToken);
        if (location is null)
            throw new NotFoundException(request.CityName);
        return location;
    }
}