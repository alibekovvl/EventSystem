using EventProcessor.Models;
using Microsoft.EntityFrameworkCore;

namespace EventProcessor.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }
    
    public DbSet<Event> Events { get; set; }
    public DbSet<Incident> Incidents { get; set; }
}