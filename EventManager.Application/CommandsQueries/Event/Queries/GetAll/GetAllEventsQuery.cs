using EventManager.Application.CommandsQueries.Event.Queries.Dtos;
using MediatR;

namespace EventManager.Application.CommandsQueries.Event.Queries.GetAll;

public class GetAllEventsQuery : IRequest<IEnumerable<GetEventDto>> { }