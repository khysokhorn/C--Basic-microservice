using ModelInventory;

namespace Inventor.Extention
{
    public static class Extentions
    {
        public static InventoryItemDTO asDto(this InventoryItems items)
        {
            return new InventoryItemDTO(items.CatelogItem, items.Quntity, items.AcquireDate);
        }
    }
}