using EventGenerator.Models;
using EventGenerator.Services.Interfaces;

namespace EventGenerator.Services.Implementations;

public class EventSender : IEventSender
{
    private readonly HttpClient _httpclient;
    private readonly ILogger<EventSender> _logger;

    public EventSender(HttpClient httpClient, ILogger<EventSender> logger)
    {
        _httpclient = httpClient;
        _logger = logger;
    }

    public async Task SendEventAsync(Event @event)
    {
        try
        {
            var response = await _httpclient.PostAsJsonAsync($"/api/incidents",@event);
            response.EnsureSuccessStatusCode();
            _logger.LogInformation($"Event sent: {@event.Id}, Type: {@event.Type}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send event");
            throw;
        }
    }
}