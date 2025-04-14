using EventGenerator.Models;

namespace EventGenerator.Services.Interfaces;

public interface IEventSender
{
    Task SendEventAsync(Event @event);
}