using EventGenerator.Models;
using EventGenerator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventGenerator.Controllers;
[ApiController]
[Route("api/events")]
public class EventController: ControllerBase
{
    private readonly IEventSender _eventSender;
    private readonly Random _random = new();

    [HttpPost("generate")]

    public async Task<IActionResult> GenerateEvent()
    {
        var @event = new Event()
        {
            Type = (EventTypeEnum)_random.Next(1, 4)
        };
        
        await _eventSender.SendEventAsync(@event);
        return Ok(@event);
    }
}