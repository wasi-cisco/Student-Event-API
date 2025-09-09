using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;          
using StudentEvents.Api.Data;
using StudentEvents.Api.Services;
using StudentEvents.Api.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentEvents API", Version = "v1" });
});

// EF Core
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Dependency Injection
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentEvents API v1"));
}

app.UseHttpsRedirection();

app.MapControllers();   // 👈 REQUIRED to enable controller routes

app.Run();
