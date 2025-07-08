public interface IResourceInventory
{
    /// <summary>
    /// Adds item to inventory
    /// </summary>
    void AddToInventory(ResourcePackage obj);
    void GetAllFromInventory();
    ResourceType PeekInventoryType();
    bool InventoryFull();
}
