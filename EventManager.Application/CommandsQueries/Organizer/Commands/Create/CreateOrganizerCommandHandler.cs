using AutoMapper;
using EventManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Application.CommandsQueries.Organizer.Commands.Create;

public class CreateOrganizerCommandHandler : IRequestHandler<CreateOrganizerCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEventManagerDbContext _dbContext;

    public CreateOrganizerCommandHandler(IMapper mapper,
        IEventManagerDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(CreateOrganizerCommand request, CancellationToken cancellationToken)
    {
        var existingOrganizer = await _dbContext.Organizers
            .FirstOrDefaultAsync(o => o.OrganizerName == request.OrganizerName, cancellationToken);
        if (existingOrganizer != null)
            return Unit.Value;
        await CreateOrganizer(request, cancellationToken);
        return Unit.Value;
    }
    
    private async Task CreateOrganizer(CreateOrganizerCommand request,
        CancellationToken cancellationToken)
    {
        var organizer = _mapper.Map<Domain.Organizer>(request);
        await _dbContext.Organizers.AddAsync(organizer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}