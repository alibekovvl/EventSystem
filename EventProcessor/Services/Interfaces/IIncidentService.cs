using EventProcessor.Models;

namespace EventProcessor.Services.Interfaces;

public interface IIncidentService
{
    Task ProcessEventAsync(Event @event);
    Task <List<Incident>> GetIncidentsAsync(int page, int pageSize, string? sortBy, bool descending);
}