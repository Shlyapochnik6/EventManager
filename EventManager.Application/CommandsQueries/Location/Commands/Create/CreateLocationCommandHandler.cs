using AutoMapper;
using EventManager.Application.Interfaces;
using MediatR;

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
        var location = _mapper.Map<Domain.Location>(request);
        await _dbContext.Locations.AddAsync(location, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}