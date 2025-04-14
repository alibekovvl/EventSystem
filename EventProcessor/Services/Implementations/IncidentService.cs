using EventProcessor.Data;
using EventProcessor.Models;
using EventProcessor.Services.Interfaces;
using EventProcessor.SortAndPagination;
using Microsoft.EntityFrameworkCore;

namespace EventProcessor.Services.Implementations;

public class IncidentService : IIncidentService
{
    private readonly AppDbContext _dbContext;

    public IncidentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task ProcessEventAsync(Event @event)
    {
        //Шаблон №1 
        if (@event.Type == EventTypeEnum.Type1)
        {
            var incident = new Incident()
            {
                Type = IncidentTypeEnum.Type1,
                Time = @event.Time
            };
            await _dbContext.Incidents.AddAsync(incident);
            await _dbContext.SaveChangesAsync();
        }
        
        //Шаблон №2 
        if (@event.Type == EventTypeEnum.Type2)
        {
            var type2EventTime = @event.Time;

            var relatedTime = await _dbContext.Events
                .Where(e => e.Type == EventTypeEnum.Type1 && e.Time <= type2EventTime.AddSeconds(20))
                .FirstOrDefaultAsync();

            Incident incident;
            if (relatedTime != null)
            {
                incident = new Incident
                {
                    Type = IncidentTypeEnum.Type2,
                    Time = @event.Time
                };
            }
            else
            {
                incident = new Incident
                {
                    Type = IncidentTypeEnum.Type1,
                    Time = @event.Time
                };
            }
            await _dbContext.Incidents.AddAsync(incident);
            await _dbContext.SaveChangesAsync();
        }
        
        //Шаблон №3 
        if (@event.Type == EventTypeEnum.Type3)
        {
            var type3EventTime = @event.Time;
            
            var relatedTime = await _dbContext.Incidents
                .Where(b => b.Type == IncidentTypeEnum.Type2 && b.Time <= type3EventTime.AddSeconds(60))
                .FirstOrDefaultAsync();
            
            Incident incident;
            if (relatedTime != null)
            {
                incident = new Incident
                {
                    Type = IncidentTypeEnum.Type3,
                    Time = @event.Time
                };
            }
            else
            {
                incident = new Incident
                {
                    Type = IncidentTypeEnum.Type1,
                    Time = @event.Time
                };
            }
            await _dbContext.Incidents.AddAsync(incident);
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task<List<Incident>> GetIncidentsAsync(int page, int pageSize, string? sortBy, bool descending)
    {
        var query = _dbContext.Incidents.Include(i => i.Events).AsQueryable();
        var pageParams = new PageParams{Page = page, PageSize = pageSize};
        var sortParams = new SortParams{SortBy = sortBy ?? "Time", Descending = descending};
        
        query = query.ApplySorting(sortParams).ApplyPaging(pageParams);
        
        return await query.ToListAsync();
    }
}