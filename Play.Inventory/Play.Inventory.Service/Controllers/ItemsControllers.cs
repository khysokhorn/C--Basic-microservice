using Commom.Repository;
using Inventor.Extention;
using Microsoft.AspNetCore.Mvc;
using ModelInventory;

namespace Inventory.Controller
{
    [ApiController]
    [Route("items")]
    public class ItemsControllers : ControllerBase
    {
        private readonly IRepository<InventoryItems> repository;
        public ItemsControllers(IRepository<InventoryItems> repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDTO>>> GetAsync(Guid UserID)
        {
            if (UserID == Guid.Empty)
            {
                return BadRequest();
            }
            var items = (await repository.GetAllAsync(item => item.UserID == UserID, 1, 20))
            .Select(item => item.asDto());
            return Ok(items);
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