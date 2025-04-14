using EventGenerator.Models;
using EventGenerator.Services.Interfaces;

namespace EventGenerator.Services;

public class EventGeneratorService : BackgroundService
{
    private readonly IEventSender _eventSender;
    private readonly Random _random = new();
    private readonly TimeSpan _delay = TimeSpan.FromSeconds(2);
    private DateTime _lastEventTime = DateTime.UtcNow;

    public EventGeneratorService(IEventSender eventSender)
    {
        _eventSender = eventSender;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            var timeSinceLastEvent = DateTime.UtcNow - _lastEventTime;
            if (timeSinceLastEvent >= _delay)
            {
                await GenerateAndSendEventsAsync();
                continue;
            } // Генерация, если прошло больше 2 сек.
            //Иначе ждем ост. время (0 ;(2000 - timesinselastevent))
            await Task.Delay (_random.Next(0, (int)(_delay - timeSinceLastEvent).TotalMilliseconds),stoppingToken);
            await GenerateAndSendEventsAsync();
        }
        var @event = new Event()
        {
            Type = (EventTypeEnum)_random.Next(1, 4)
        };
        await _eventSender.SendEventAsync(@event);
    }
    private async Task GenerateAndSendEventsAsync()
    {
        var @event = new Event()
        {
            Type = (EventTypeEnum)_random.Next(1, 4),
            Time = DateTime.UtcNow
        };
        await _eventSender.SendEventAsync(@event);
        _lastEventTime = @event.Time;
    }
}