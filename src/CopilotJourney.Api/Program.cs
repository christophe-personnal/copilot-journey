using CopilotJourney.Api.Models;
using CopilotJourney.Api.Services;
using CopilotJourney.Application.Interfaces;
using CopilotJourney.Infrastructure.Services;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add memory cache and our cache abstraction
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IWeatherCache, MemoryWeatherCache>();

// Controllers
builder.Services.AddControllers();

// Configure OpenMeteo HttpClient with Polly retry
var openMeteoBase = builder.Configuration["OpenMeteo:BaseUrl"] ?? "https://api.open-meteo.com/";
builder.Services.AddHttpClient<IOpenMeteoClient, OpenMeteoClient>(client =>
{
    client.BaseAddress = new Uri(openMeteoBase);
    client.DefaultRequestHeaders.UserAgent.ParseAdd("CopilotJourney/1.0");
})
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(4) }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
