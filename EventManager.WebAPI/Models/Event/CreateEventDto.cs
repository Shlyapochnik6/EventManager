using AutoMapper;
using EventManager.Application.CommandsQueries.Event.Commands.Create;
using EventManager.Application.Common.Mappings;

namespace EventManager.WebAPI.Models.Event;

public class CreateEventDto : IMapWith<CreateEventCommand>
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
        profile.CreateMap<CreateEventDto, CreateEventCommand>()
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
                opt => opt.MapFrom(e => e.Speaker))
            .ForMember(u => u.Organizer,
                opt => opt.MapFrom(e => e.Organizer))
            .ForMember(u => u.Location,
                opt => opt.MapFrom(e => e.Location));
    }
}