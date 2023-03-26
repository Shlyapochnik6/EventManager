using AutoMapper;
using AutoMapper.QueryableExtensions;
using EventManager.Application.CommandsQueries.Event.Queries.Dtos;
using EventManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Application.CommandsQueries.Event.Queries.GetAll;

public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, IEnumerable<GetEventDto>>
{
    private readonly IMapper _mapper;
    private readonly IEventManagerDbContext _dbContext;

    public GetAllEventsQueryHandler(IEventManagerDbContext dbContext,
        IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<GetEventDto>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        var eventsDtos = await _dbContext.Events
            .Include(e => e.Organizer)
            .Include(e => e.Location)
            .Include(e => e.Speaker)
            .ProjectTo<GetEventDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return eventsDtos;
    }
}