using AutoMapper;
using EventManager.Application.Common.Mappings;
using MediatR;

namespace EventManager.Application.CommandsQueries.Event.Commands.Create;

public class CreateEventCommand : IRequest<Guid>, IMapWith<Domain.Event>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Plan { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Speaker { get; set; }

    public string Organizer { get; set; }

    public string Location { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateEventCommand, Domain.Event>()
            .ForMember(e => e.Name,
                opt => opt.MapFrom(e => e.Name))
            .ForMember(e => e.Description,
                opt => opt.MapFrom(e => e.Description))
            .ForMember(e => e.Plan,
                opt => opt.MapFrom(e => e.Plan))
            .ForMember(e => e.StartTime,
                opt => opt.MapFrom(e => e.StartTime))
            .ForMember(e => e.EndTime,
                opt => opt.MapFrom(e => e.EndTime))
            .ForMember(u => u.Speaker,
                opt => opt.Ignore())
            .ForMember(u => u.Organizer,
                opt => opt.Ignore())
            .ForMember(u => u.Location,
                opt => opt.Ignore());
    }
}