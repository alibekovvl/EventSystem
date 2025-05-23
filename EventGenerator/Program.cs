using EventGenerator.Data;
using EventGenerator.Services;
using EventGenerator.Services.Implementations;
using EventGenerator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<IEventSender,EventSender>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5081");
});

builder.Services.AddHostedService<EventGeneratorService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data source=incidents.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
