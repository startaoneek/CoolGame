using CoolGame.Server.DataAccess.Entities;
using CoolGame.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoolGame.Server.Controllers;

[ApiController]
[Route("api")]
public class ServerController : ControllerBase
{
    private readonly IEventsService _eventsService;
    private readonly IOffersService _offersService;


    public ServerController(IEventsService eventsService, IOffersService offersService)
    {
        _eventsService = eventsService;
        _offersService = offersService;
    }

    [HttpGet("offers")]
    public async Task<ActionResult<IEnumerable<Offer>>> GetOffers()
    {
        return await _offersService.GetOffers();
    }

    [HttpPost("offers")]
    public async Task<ActionResult<Guid>> AddOffer([FromBody]Offer offer)
    {
        return await _offersService.AddOffer(offer);
    }

    [HttpGet("events")]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
    {
        return await _eventsService.GetEvents();
    }

    [HttpPost("events")]
    public async Task<ActionResult<Guid>> AddEvent([FromBody]Event @event)
    {
        return await _eventsService.AddEvent(@event);
    }
}