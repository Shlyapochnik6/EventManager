using EventManager.Application.Common.Exceptions;
using EventManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Application.CommandsQueries.Speaker.Queries.Get;

public class GetSpeakerQueryHandler : IRequestHandler<GetSpeakerQuery, Domain.Speaker>
{
    private readonly IEventManagerDbContext _dbContext;

    public GetSpeakerQueryHandler(IEventManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.Speaker> Handle(GetSpeakerQuery request, CancellationToken cancellationToken)
    {
        var speaker = await _dbContext.Speakers
            .FirstOrDefaultAsync(l => l.SpeakerName == request.SpeakerName, cancellationToken);
        if (speaker is null)
            throw new NotFoundException(request.SpeakerName);
        return speaker;
    }
}