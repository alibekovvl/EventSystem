using EventProcessor.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace EventProcessor.SortAndPagination;

public static class IncidentExtentions
{
    public static IQueryable<Incident> ApplySorting(this IQueryable<Incident> query, SortParams sortParams)
    {
        return sortParams.SortBy.ToLower() switch
        {
            "type" => sortParams.Descending ? query.OrderByDescending(i => i.Type) : query.OrderBy(i => i.Type),
            "time" or _ => sortParams.Descending ? query.OrderByDescending(i => i.Time) : query.OrderBy(i => i.Time),
        };
    }
    public static IQueryable<Incident> ApplyPaging(this IQueryable<Incident> query, PageParams pageParams)
    {
        var skip = (pageParams.Page - 1) * pageParams.PageSize;
        return query.Skip(skip).Take(pageParams.PageSize);
    }
}