using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    NULL,
    WOOD,
    STONE,
    IRON,
}

public class ResourcePackage
{
    ResourceType _type;
    public ResourceType Type { get { return _type; } }
    int _amount;
    public int Amount { get { return _amount; } }

    public ResourcePackage(ResourceType type, int amount = 0) 
    {
        _type = type;
        _amount = amount;
    }
    public void ChangeResourceAmount(int change) => _amount += change;
}
