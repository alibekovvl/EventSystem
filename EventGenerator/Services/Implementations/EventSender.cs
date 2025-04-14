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

    public async Task<bool> SendEventAsync(Event @event)
    {
        const int maxRetries = 5;
        var delay = TimeSpan.FromSeconds(2);

        for (int i = 0; i <= maxRetries; i++)
        {
            try
            {
                var response = await _httpclient.PostAsJsonAsync($"/api/incidents", @event);
                if (response.IsSuccessStatusCode)
                    return true;
                _logger.LogError($"Error sending event: {@event}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while sending event. Attempt {i}", i);
            }

            if (i < maxRetries)
            {
                await Task.Delay(delay);
                delay = delay * 2;
            }
        }

        _logger.LogError($"Max retries reached. Failed to send event: {@event}", @event);
        
        return false;
    }
}