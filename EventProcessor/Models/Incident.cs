namespace EventProcessor.Models;

public class Incident
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public IncidentTypeEnum Type { get; set; }
    public DateTime Time { get; set; } = DateTime.UtcNow;
    public List<Event> Events { get; set; } = new();
}
public enum IncidentTypeEnum { Type1 = 1, Type2 = 2, Type3 = 3 }