using Blazor2App.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Blazor2App.Server.Controllers.V2;

[ApiController]
[Route(BaseWeathersRoute)]
[Asp.Versioning.ApiVersion(2.0)]
public class WeatherForecastController : AllphiControllerBase
{
    protected const string BaseWeathersRoute = BaseRoute + "weather";
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]

    public IEnumerable<WeatherForecast> Get2()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
