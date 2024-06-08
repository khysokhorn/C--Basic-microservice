using Entities;

namespace ModelInventory
{
    public class InventoryItems : IEntity
    {
        public string Id { get; set; }
        public Guid UserID { get; set; }
        public string CatelogItem { get; set; }
        public int Quntity { get; set; }
        public DateTimeOffset AcquireDate { get; set; }
    }
}