namespace EventProcessor.Models;

public class IncidentPr
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public IncidentTypeEnum Type { get; set; }
    public DateTime Time { get; set; } = DateTime.UtcNow;
    public List<EventPr> Events { get; set; } = new();
}
public enum IncidentTypeEnum { Type1 = 1, Type2 = 2, Type3 = 3 }