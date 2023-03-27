using AutoMapper;
using EventManager.Application.CommandsQueries.Event.Queries.Get;
using EventManager.Application.CommandsQueries.Location.Commands.Create;
using EventManager.Application.CommandsQueries.Location.Queries.Get;
using EventManager.Application.CommandsQueries.Organizer.Commands.Create;
using EventManager.Application.CommandsQueries.Organizer.Queries.Get;
using EventManager.Application.CommandsQueries.Speaker.Commands.Create;
using EventManager.Application.CommandsQueries.Speaker.Queries.Get;
using EventManager.Application.Interfaces;
using MediatR;

namespace EventManager.Application.CommandsQueries.Event.Commands.Update;

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IEventManagerDbContext _dbContext;

    public UpdateEventCommandHandler(IMapper mapper, IMediator mediator,
        IEventManagerDbContext dbContext)
    {
        _mapper = mapper;
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(UpdateEventCommand request, 
        CancellationToken cancellationToken)
    {
        await CreateSpeaker(request.Speaker);
        await CreateLocation(request.Location);
        await CreateOrganizer(request.Organizer);
        await GetUpdatedEvent(request, cancellationToken);
        return Unit.Value;
    }

    private async Task<Domain.Event> GetEvent(Guid eventId,
        CancellationToken cancellationToken)
    {
        var query = new GetEventQuery(eventId);
        return await _mediator.Send(query, cancellationToken);
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

    private async Task GetUpdatedEvent(UpdateEventCommand request,
        CancellationToken cancellationToken)
    {
        var cityEvent = await GetEvent(request.EventId, cancellationToken);
        var updatedEvent = _mapper.Map(request, cityEvent);
        updatedEvent.Organizer = await GetOrganizer(request.Organizer);
        updatedEvent.Location = await GetLocation(request.Location);
        updatedEvent.Speaker = await GetSpeaker(request.Speaker);
        _dbContext.Events.Update(updatedEvent);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}