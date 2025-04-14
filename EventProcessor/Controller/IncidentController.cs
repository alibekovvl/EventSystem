using EventProcessor.Models;
using EventProcessor.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventProcessor.Controller;
[ApiController]
[Route("api/incidents")]
public class IncidentController : ControllerBase
{
    private readonly IIncidentService _incidentService;

    public IncidentController(IIncidentService incidentService)
    {
        _incidentService = incidentService;
    }
    [HttpGet]
    public async Task<IActionResult> GetIncidents()
    {
        var incidents = await _incidentService.GetIncidentsAsync();
        return Ok(incidents);
    }
    [HttpPost]
    public async Task<IActionResult> ProcessEvent([FromBody] Event @event)
    {
        await _incidentService.ProcessEventAsync(@event);
        return Ok();
    }
}