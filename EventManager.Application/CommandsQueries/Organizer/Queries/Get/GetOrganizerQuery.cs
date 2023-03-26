using MediatR;

namespace EventManager.Application.CommandsQueries.Organizer.Queries.Get;

public class GetOrganizerQuery : IRequest<Domain.Organizer>
{
    public string OrganizerName { get; set; }

    public GetOrganizerQuery(string organizerName)
    {
        OrganizerName = organizerName;
    }
}