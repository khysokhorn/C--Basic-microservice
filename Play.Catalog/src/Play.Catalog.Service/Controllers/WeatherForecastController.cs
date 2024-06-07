using Entities;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IItemRepository itemRepository;
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IItemRepository _iitemRepository)
    {
        _logger = logger;
        itemRepository = _iitemRepository;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<MovieItemDTO>> GetAllAsync()
    {
        var items = (await itemRepository.GetAllAsync(
            page: 1, pageSize: 10
        ))
        .Select(item => item.AsDto());
        return items;
    }
}
