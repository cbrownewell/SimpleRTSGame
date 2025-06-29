public interface IResourceInventory
{
    /// <summary>
    /// Adds item to inventory
    /// </summary>
    void AddToInventory(Resource obj);
    void GetAllFromInventory();
    ResourceType PeekInventoryType();
    bool InventoryFull();
}
