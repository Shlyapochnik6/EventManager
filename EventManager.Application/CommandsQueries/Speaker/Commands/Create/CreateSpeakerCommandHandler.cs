using AutoMapper;
using EventManager.Application.Interfaces;
using MediatR;

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
    
    public async Task<Unit> Handle(CreateSpeakerCommand request, CancellationToken cancellationToken)
    {
        var speaker = _mapper.Map<Domain.Speaker>(request);
        await _dbContext.Speakers.AddAsync(speaker, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}