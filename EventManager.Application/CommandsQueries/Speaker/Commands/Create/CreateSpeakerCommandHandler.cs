using AutoMapper;
using EventManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Application.CommandsQueries.Speaker.Commands.Create;

public class CreateSpeakerCommandHandler : IRequestHandler<CreateSpeakerCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEventManagerDbContext _dbContext;

    public CreateSpeakerCommandHandler(IMapper mapper,
        IEventManagerDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(CreateSpeakerCommand request,
        CancellationToken cancellationToken)
    {
        var existingSpeaker = await _dbContext.Speakers
            .FirstOrDefaultAsync(s => s.SpeakerName == request.SpeakerName, cancellationToken);
        if (existingSpeaker != null)
            return Unit.Value;
        await CreateSpeaker(request, cancellationToken);
        return Unit.Value;
    }

    private async Task CreateSpeaker(CreateSpeakerCommand request,
        CancellationToken cancellationToken)
    {
        var speaker = _mapper.Map<Domain.Speaker>(request);
        await _dbContext.Speakers.AddAsync(speaker, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}