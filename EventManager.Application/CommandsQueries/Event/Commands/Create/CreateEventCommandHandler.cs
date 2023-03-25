using MediatR;

namespace EventManager.Application.CommandsQueries.Event.Commands.Create;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
{
    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}