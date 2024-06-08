using ModelInventory;

namespace Inventor.Extention
{
    public static class Extentions
    {
        public static InventoryItemDTO asDto(this InventoryItems items,
         string Imdb, string Year, string Quality
        )
        {
            return new InventoryItemDTO(Imdb, Year, Quality, items.CatelogItem, items.Quntity, items.AcquireDate, items?.Id?.ToString());
        }
    }
}