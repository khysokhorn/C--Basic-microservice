using Commom.Repository;
using Inventor;
using Inventor.Extention;
using Microsoft.AspNetCore.Mvc;
using ModelInventory;
using MongoDB.Driver;

namespace Inventory.Controller
{
    [ApiController]
    [Route("items")]
    public class ItemsControllers : ControllerBase
    {
        private readonly IRepository<InventoryItems> repository;
        private readonly CatelogClient catelogClient;
        public ItemsControllers(IRepository<InventoryItems> repository, CatelogClient catelogClient)
        {
            this.repository = repository;
            this.catelogClient = catelogClient;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDTO>>> GetAsync(Guid? UserID)
        {
            var items = await catelogClient.GetcatelogItemsAsync();
            var userMovieCountEntities = await repository.GetAllAsync(item => item.UserID == UserID, 1, 20);
            var a = items.Single(s => s.ID == "/movie/watch-nowhere-online-100843");
            var inventoryItemDTO = userMovieCountEntities?.Select(initem =>
                {
                    CatelogItemsDto? catelogItems;
                    try
                    {
                        catelogItems = items?.Single(c => c?.ID == initem?.CatelogItem);
                    }
                    catch (System.Exception)
                    {
                        catelogItems = null;
                    }
                    return initem?.asDto(catelogItems?.Imdb, catelogItems?.Year, catelogItems?.Quality);
                }
            );
            return Ok(inventoryItemDTO);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(GratItemDto gratItemDto)
        {
            var inventoryItem = await repository.GetAsync(item => item.UserID == gratItemDto.userID && item.CatelogItem == gratItemDto.CatalogItemdId);
            if (inventoryItem == null)
            {
                // create new 
                inventoryItem = new InventoryItems
                {
                    CatelogItem = gratItemDto.CatalogItemdId,
                    UserID = gratItemDto.userID,
                    Quntity = gratItemDto.Quntity,
                    AcquireDate = DateTimeOffset.UtcNow,
                    Id = Guid.NewGuid().ToString()
                };
                await repository.CreateAsync(inventoryItem);
            }
            else
            {
                inventoryItem.Quntity += gratItemDto.Quntity;

                await repository.UpdateAsync(inventoryItem);
            }
            return Ok();
        }

    }
}