using Entities;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IRepository<MovieItem> itemRepository;


    public WeatherForecastController(IRepository<MovieItem> _iitemRepository)
    {
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
