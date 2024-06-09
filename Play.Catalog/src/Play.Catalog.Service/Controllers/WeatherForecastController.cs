using System.Reflection;
using Commom.Repository;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Model;
using Play.Contracts;
namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("movies")]
public class WeatherForecastController : ControllerBase
{
    private readonly IRepository<MovieItem> itemRepository;
    protected readonly MassTransit.IPublishEndpoint publishEndpoint;

    public WeatherForecastController(IRepository<MovieItem> _iitemRepository,
    MassTransit.IPublishEndpoint publishEndpoint
    )
    {
        itemRepository = _iitemRepository;
        this.publishEndpoint = publishEndpoint;
    }

    [HttpGet("items")]
    public async Task<IEnumerable<MovieItemDTO>> GetAllAsync()
    {
        var items = (await itemRepository.GetAllAsync(
            page: 1, pageSize: 10
        ))
        .Select(item => item.AsDto());
        return items;
    }

    [HttpPost("items")]
    public async Task<ActionResult> PostMovieAsync(MovieItem item)
    {
        await itemRepository.CreateAsync(item);
        var catelog = new CatelogItemCreated(
            ID: item.Id, Name: item.Title, Description: "Description"
        );
        await publishEndpoint.Publish<CatelogItemCreated>(catelog);
        return Ok();
    }


    [HttpPut("items")]
    public async Task<ActionResult> UpdateMovieAsync(MovieItem item)
    {
        var existingItems = await itemRepository.GetAsync(item.Id);
        existingItems.Count = item.Count;
        await itemRepository.UpdateAsync(existingItems);
        var catelog = new CatelogItemUpdated(
            ID: existingItems.Id, Name: existingItems.Title, Description: "Description"
        );
        
        await publishEndpoint.Publish(catelog);
        return Ok();
    }
}


