using EventGenerator.Models;
using EventProcessor.Models;

namespace EventProcessor.Services.Interfaces;

public interface IIncidentService
{
    Task ProcessEventAsync(Event @event);
    Task <List<IncidentPr>> GetIncidentsAsync(int page, int pageSize, string? sortBy, bool descending);
}