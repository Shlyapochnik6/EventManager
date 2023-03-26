using AutoMapper;
using EventManager.Application.CommandsQueries.Location.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.WebAPI.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore
    .Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
[Route("api/location")]
public class LocationController : BaseController
{
    public LocationController(IMapper mapper, IMediator mediator) :
        base(mapper, mediator) { }
   
    [HttpPost]
    public async Task<ActionResult> Create(string cityName)
    {
        var command = new CreateLocationCommand { CityName = cityName };
        await Mediator.Send(command);
        return Ok();
    }
}