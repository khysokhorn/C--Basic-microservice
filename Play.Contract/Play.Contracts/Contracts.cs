namespace Play.Contracts
{
    public record CatelogItemCreated(string ID, string Name, string Description);
    public record CatelogItemUpdated(string ID, string Name, string Description);
    public record CatelogItemDeleted(string ID);
}