using EventGenerator.Data;
using EventProcessor.Services.Implementations;
using EventProcessor.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data source=incidents.db"));

builder.Services.AddScoped<IIncidentService, IncidentService>();
builder.Services.AddControllers()
    .ConfigureApplicationPartManager(apm =>
    {
        // Находим и удаляем сборку, содержащую контроллеры из EventGenerator
        var generatorAssemblyName = typeof(EventGenerator.Controllers.EventController).Assembly.GetName().Name;
        var part = apm.ApplicationParts.FirstOrDefault(p => p.Name == generatorAssemblyName);
        if (part != null)
        {
            apm.ApplicationParts.Remove(part);
        }
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
    
