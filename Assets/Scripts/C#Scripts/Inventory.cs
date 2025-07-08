public class Inventory 
{
    ResourcePackage _resource;

    /// <summary>
    /// Adds resources to inventory
    /// </summary>
    /// <param name="pckg"></param>
    /// <returns>True when the resource is added successfully</returns>
    public bool AddToInventory(ResourcePackage pckg) 
    {
        if (_resource == null) 
        { 
            _resource = pckg; 
            return true; 
        }
        else if (_resource.Type == pckg.Type)
        {
            _resource.ChangeResourceAmount(pckg.Amount);
            return true;
        }

        return false;
    }

    public ResourceType PeekType() { return _resource.Type; }
    public bool InventoryFull => _resource.Amount > 0;//Need to implement a way of max carry limit.  MySQL?

    /// <summary>
    /// Pops the stored Resource object out of the Inventory
    /// </summary>
    /// <returns></returns>
    public ResourcePackage RetrieveAllInventory() 
    {
        ResourcePackage retreive = new ResourcePackage(_resource.Type, _resource.Amount);
        _resource = null;
        return retreive;
    }
}
