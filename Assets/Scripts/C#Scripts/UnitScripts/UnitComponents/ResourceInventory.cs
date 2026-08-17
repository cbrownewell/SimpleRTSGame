using UnityEngine;

public class ResourceInventory : MonoBehaviour, IResourceInventory
{
    ResourceType _resource = ResourceType.NULL;

    public bool AddToInventory(ResourceType resource)
    {
        if (_resource == ResourceType.NULL)
        {
            _resource = resource;
            return true;
        }
        return false;
    }

    public ResourceType GetResourceFromInventory()
    {
        var copy = _resource;
        _resource = ResourceType.NULL;
        return copy;
    }

    public bool InventoryFull() => PeekResourceType() != ResourceType.NULL;
    public ResourceType PeekResourceType() => _resource;

}
