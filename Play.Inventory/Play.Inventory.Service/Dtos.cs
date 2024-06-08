namespace ModelInventory
{
    public record GratItemDto(Guid userID, string CatalogItemdId, int Quntity);
    public record InventoryItemDTO(string CatalogItemdId, int Quntity, DateTimeOffset acquiredDate);
}