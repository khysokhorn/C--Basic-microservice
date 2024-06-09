using Entities;

namespace ModelInventory
{
    public class CatalogItem : IEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
    }
}