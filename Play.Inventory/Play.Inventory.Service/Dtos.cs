namespace ModelInventory
{
    public record GratItemDto(Guid userID, string CatalogItemdId, int Quntity);
    public record InventoryItemDTO(
         string Imdb, string Year, string Quality,
        string CatalogItemdId, int Quntity, DateTimeOffset acquiredDate, string ID
    );

    public record CatelogItemsDto(
           int Quntity, DateTimeOffset acquiredDate, string ID,
           string Imdb, string Year, string Quality
       );
}


//  [BsonElement("imdb")]
// public string Imdb { get; set; }

// [BsonElement("year")]
// public string Year { get; set; }

// [BsonElement("quality")]
// public string Quality { get; set; }