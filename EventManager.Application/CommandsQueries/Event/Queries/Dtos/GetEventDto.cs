using AutoMapper;
using EventManager.Application.Common.Mappings;

namespace EventManager.Application.CommandsQueries.Event.Queries.Dtos;

public class GetEventDto : IMapWith<Domain.Event>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Plan { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string SpeakerName { get; set; }

    public string OrganizerName { get; set; }

    public string Location { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Event, GetEventDto>()
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
            .ForMember(u => u.SpeakerName,
                opt => opt.MapFrom(e => e.Speaker.SpeakerName))
            .ForMember(u => u.OrganizerName,
                opt => opt.MapFrom(e => e.Organizer.OrganizerName))
            .ForMember(u => u.Location,
                opt => opt.MapFrom(e => e.Location.CityName));
    }
}