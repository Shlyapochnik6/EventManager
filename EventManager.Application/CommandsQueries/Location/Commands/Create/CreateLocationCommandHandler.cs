using AutoMapper;
using EventManager.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Application.CommandsQueries.Location.Commands.Create;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEventManagerDbContext _dbContext;

    public CreateLocationCommandHandler(IMapper mapper,
        IEventManagerDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(CreateLocationCommand request,
        CancellationToken cancellationToken)
    {
        var existingLocation = await _dbContext.Locations
            .FirstOrDefaultAsync(l => l.CityName == request.CityName, cancellationToken);
        if (existingLocation != null)
            return Unit.Value;
        await CreateLocation(request, cancellationToken);
        return Unit.Value;
    }
    
    private async Task CreateLocation(CreateLocationCommand request,
        CancellationToken cancellationToken)
    {
        var location = _mapper.Map<Domain.Location>(request);
        await _dbContext.Locations.AddAsync(location, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}