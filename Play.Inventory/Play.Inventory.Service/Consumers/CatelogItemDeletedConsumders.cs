using Commom.Repository;
using MassTransit;
using ModelInventory;
using Play.Contracts;

public class CatelogItemDeletedConsumders : IConsumer<CatelogItemCreated>
{
    private readonly IRepository<CatalogItem> repository;

    public CatelogItemDeletedConsumders(IRepository<CatalogItem> repository)
    {
        this.repository = repository;
    }
    public async Task Consume(ConsumeContext<CatelogItemCreated> context)
    {
        var message = context.Message;
        var item = await repository.GetAsync(message.ID);
        if (item == null)
        {
            item = new CatalogItem
            {
                Id = message.ID,
                Title = message.Name,
                Year = message.Description,
            };
            await repository.CreateAsync(item);
        }
        else
        {
            await repository.RemoveAsync(item.Id);
        }

    }
}