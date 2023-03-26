using MediatR;

namespace EventManager.Application.CommandsQueries.Speaker.Queries.Get;

public class GetSpeakerQuery: IRequest<Domain.Speaker>
{
    public string SpeakerName { get; set; }

    public GetSpeakerQuery(string speakerName)
    {
        SpeakerName = speakerName;
    }
}