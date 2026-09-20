

public interface IResourceInventory
{
    /// <summary>
    /// Adds resources to inventory
    /// </summary>
    /// <param name="pckg"></param>
    /// <returns>True when the resource is added successfully</returns>
    bool AddToInventory(ResourceType resource);
    /// <summary>
    /// Gets the stored resource and removes
    /// </summary>
    /// <returns></returns>
    ResourceType GetResourceFromInventory();
    /// <summary>
    /// Looks at the currently held resource's type
    /// </summary>
    /// <returns></returns>
    ResourceType PeekResourceType();
    /// <summary>
    /// Returns true if there is a resource in inventory
    /// </summary>
    /// <returns></returns>
    bool InventoryFull();
}
