using AutoMapper;
using EventManager.Application.Common.Mappings;
using MediatR;

namespace EventManager.Application.CommandsQueries.Speaker.Commands.Create;

public class CreateSpeakerCommand : IRequest<Unit>, IMapWith<Domain.Speaker>
{
    public string SpeakerName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateSpeakerCommand, Domain.Speaker>()
            .ForMember(s => s.SpeakerName,
                opt => opt.MapFrom(s => s.SpeakerName));
    }
}