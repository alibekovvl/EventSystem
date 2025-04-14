using EventGenerator.Models;
using EventGenerator.Services.Interfaces;

namespace EventGenerator.Services;

public class EventGeneratorService : BackgroundService
{
    private readonly IEventSender _eventSender;
    private readonly Random _random = new();
    private readonly TimeSpan _delay = TimeSpan.FromSeconds(2);
    

    public EventGeneratorService(IEventSender eventSender)
    {
        _eventSender = eventSender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay (_random.Next(0, (int)_delay.TotalMilliseconds),stoppingToken);
        }

        var @event = new Event()
        {
            Type = (EventTypeEnum)_random.Next(1, 4)
        };
        
        await _eventSender.SendEventAsync(@event);
        
    }
}