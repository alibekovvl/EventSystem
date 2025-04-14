namespace EventProcessor.DTO;

public class IncidentQueryParams
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } = "Time";
    public bool Descending { get; set; } = true;
}