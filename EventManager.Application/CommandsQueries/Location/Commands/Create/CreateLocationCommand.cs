using AutoMapper;
using EventManager.Application.Common.Mappings;
using MediatR;

namespace EventManager.Application.CommandsQueries.Location.Commands.Create;

public class CreateLocationCommand : IRequest<Unit>, IMapWith<Domain.Location>
{
    public string CityName { get; set; }

    public CreateLocationCommand() { }
    
    public CreateLocationCommand(string cityName)
    {
        CityName = cityName;
    }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateLocationCommand, Domain.Location>()
            .ForMember(l => l.CityName,
                opt => opt.MapFrom(l => l.CityName));
    }
}