public class Inventory 
{
    Resource _resource = Resource.NullResource;

    /// <summary>
    /// Adds resource(s) to inventory
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool AddToInventory(Resource obj) 
    {
        if (_resource._type == ResourceType.NULL)
        {
            _resource = obj;
            return true;
        }
        else 
        {
            if (obj._type == _resource._type)
            {
                _resource = new Resource(_resource._type, _resource._amount + obj._amount);
                return true;
            }
        }
        return false;
    }

    public ResourceType PeekType() { return _resource._type; }
    public bool InventoryFull => _resource._amount > 0;

    /// <summary>
    /// Pops the stored Resource object out of the Inventory
    /// </summary>
    /// <returns></returns>
    public Resource RetrieveAllInventory() 
    {
        Resource retreive = _resource;
        _resource = Resource.NullResource;
        return retreive;
    }
}
