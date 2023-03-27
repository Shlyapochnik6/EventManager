using AutoMapper;
using EventManager.Application.CommandsQueries.Event.Queries.Dtos;
using EventManager.Application.CommandsQueries.Event.Queries.Get;
using EventManager.Application.Interfaces;
using MediatR;

namespace EventManager.Application.CommandsQueries.Event.Queries.GetDto;

public class GetEventDtoQueryHandler : IRequestHandler<GetEventDtoQuery, GetEventDto>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IEventManagerDbContext _dbContext;

    public GetEventDtoQueryHandler(IEventManagerDbContext dbContext,
        IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    public async Task<GetEventDto> Handle(GetEventDtoQuery request, CancellationToken cancellationToken)
    {
        var cityEvent = await GetEvent(request.EventId, cancellationToken);
        return _mapper.Map<GetEventDto>(cityEvent);
    }
    
    private async Task<Domain.Event> GetEvent(Guid eventId, CancellationToken cancellationToken)
    {
        var query = new GetEventQuery(eventId);
        var cityEvent = await _mediator.Send(query, cancellationToken);
        return cityEvent;
    }
}