using AutoMapper;
using EventManager.Application.Common.Mappings;
using MediatR;

namespace EventManager.Application.CommandsQueries.Organizer.Commands.Create;

public class CreateOrganizerCommand : IRequest<Unit>, IMapWith<Domain.Organizer>
{
    public string OrganizerName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrganizerCommand, Domain.Organizer>()
            .ForMember(l => l.OrganizerName,
                opt => opt.MapFrom(l => l.OrganizerName));
    }
}