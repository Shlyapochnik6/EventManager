using AutoMapper;
using EventManager.Application.CommandsQueries.Location.Commands.Create;
using EventManager.Application.CommandsQueries.Location.Queries.Get;
using EventManager.Application.CommandsQueries.Organizer.Commands.Create;
using EventManager.Application.CommandsQueries.Organizer.Queries.Get;
using EventManager.Application.CommandsQueries.Speaker.Commands.Create;
using EventManager.Application.CommandsQueries.Speaker.Queries.Get;
using EventManager.Application.Common.Exceptions;
using EventManager.Application.Interfaces;
using MediatR;

namespace EventManager.Application.CommandsQueries.Event.Commands.Create;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IEventManagerDbContext _dbContext;

    public CreateEventCommandHandler(IMapper mapper, IMediator mediator,
        IEventManagerDbContext dbContext)
    {
        _mapper = mapper;
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateEventCommand request,
        CancellationToken cancellationToken)
    {
        await CreateSpeaker(request.Speaker);
        await CreateLocation(request.Location);
        await CreateOrganizer(request.Organizer);
        return await CreateEvent(request, cancellationToken);
    }
    
    private async Task<Domain.Location> GetLocation(string cityName)
    {
        var query = new GetLocationQuery(cityName);
        return await _mediator.Send(query);
    }
    
    private async Task<Domain.Speaker> GetSpeaker(string speakerName)
    {
        var query = new GetSpeakerQuery(speakerName);
        return await _mediator.Send(query);
    }
    
    private async Task<Domain.Organizer> GetOrganizer(string organizerName)
    {
        var query = new GetOrganizerQuery(organizerName);
        return await _mediator.Send(query);
    }
    
    private async Task CreateSpeaker(string speakerName)
    {
        var command = new CreateSpeakerCommand() { SpeakerName = speakerName };
        await _mediator.Send(command);
    }
    
    private async Task CreateOrganizer(string organizerName)
    {
        var command = new CreateOrganizerCommand(organizerName);
        await _mediator.Send(command);
    }
    
    private async Task CreateLocation(string cityName)
    {
        var command = new CreateLocationCommand(cityName);
        await _mediator.Send(command);
    }

    private async Task<Guid> CreateEvent(CreateEventCommand request,
        CancellationToken cancellationToken)
    {
        if (request.StartTime < DateTime.UtcNow || request.EndTime <= DateTime.UtcNow)
            throw new PastDateTimeException();
        var cityEvent = _mapper.Map<Domain.Event>(request);
        cityEvent.Location = await GetLocation(request.Location);
        cityEvent.Speaker = await GetSpeaker(request.Speaker);
        cityEvent.Organizer = await GetOrganizer(request.Organizer);
        await _dbContext.Events.AddAsync(cityEvent, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return cityEvent.Id;
    }
}