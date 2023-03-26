using MediatR;

namespace EventManager.Application.CommandsQueries.Location.Queries.Get;

public class GetLocationQuery : IRequest<Domain.Location>
{
    public string CityName { get; set; }

    public GetLocationQuery(string cityName)
    {
        CityName = cityName;
    }
}