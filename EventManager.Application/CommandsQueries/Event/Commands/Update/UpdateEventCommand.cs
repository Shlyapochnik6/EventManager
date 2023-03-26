using AutoMapper;
using EventManager.Application.CommandsQueries.Event.Commands.Create;
using EventManager.Application.Common.Mappings;
using MediatR;

namespace EventManager.Application.CommandsQueries.Event.Commands.Update;

public class UpdateEventCommand : IRequest<Unit>, IMapWith<Domain.Event>
{
    public Guid EventId { get; set; }
    
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Plan { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string Speaker { get; set; }

    public string Organizer { get; set; }

    public string Location { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateEventCommand, Domain.Event>()
            .ForMember(e => e.Name,
                opt => opt.Condition(src => !string.IsNullOrEmpty(src.Name)))
            .ForMember(e => e.Description,
                opt => opt.Condition(src => !string.IsNullOrEmpty(src.Description)))
            .ForMember(e => e.Plan,
                opt => opt.Condition(src => !string.IsNullOrEmpty(src.Plan)))
            .ForMember(e => e.StartTime,
                opt => opt.Condition(src => src.StartTime != default))
            .ForMember(e => e.EndTime,
                opt => opt.Condition(src => src.EndTime != default))
            .ForMember(u => u.Speaker,
                opt => opt.Ignore())
            .ForMember(u => u.Organizer,
                opt => opt.Ignore())
            .ForMember(u => u.Location,
                opt => opt.Ignore());
    }
}