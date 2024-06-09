using Commom.Repository;
using MassTransit;
using ModelInventory;
using Play.Contracts;

public class CatelogItemCreatedConsumders : IConsumer<CatelogItemCreated>
{
    private readonly IRepository<CatalogItem> repository;

    public CatelogItemCreatedConsumders(IRepository<CatalogItem> repository)
    {
        this.repository = repository;
    }
    public async Task Consume(ConsumeContext<CatelogItemCreated> context)
    {

        var message = context.Message;
        Console.WriteLine($"Consume with message {message.ID}");
        var item = await repository.GetAsync(message.ID);
        if (item != null)
        {
            return;
        }
        item = new CatalogItem
        {
            Id = message.ID,
            Title = message.Name,
            Year = message.Description,
        };
        await repository.CreateAsync(item);
    }
}