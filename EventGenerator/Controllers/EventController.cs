using EventGenerator.Models;

using EventGenerator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventGenerator.Controllers;
[ApiController]
[Route("api/events")]
public class EventController: ControllerBase
{
    private readonly IEventSender _eventSender;
    public EventController(IEventSender eventSender)
    {
        _eventSender = eventSender;
    }
    
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateEvent([FromBody] Event @event)
    {
        var sendResult = await _eventSender.SendEventAsync(@event);
        return sendResult ? Ok(@event) : BadRequest("Failed to send event");
    }
}