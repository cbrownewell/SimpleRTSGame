using UnityEngine;

public enum ResourceType
{
    NULL,
    WOOD,
    STONE,
    IRON,
}

/// <summary>
/// Used to interact with game objects who hold game resources that can be harvested/mined
/// </summary>
public interface IHarvestable 
{
    /// <summary>
    /// Removes one resource
    /// </summary>
    /// <returns>Type of resource extracted</returns>
    ResourceType Extract();
    bool IsEmpty();
    ResourceType GetResourceType();
    float GetBaseHarvestTime();

    public Transform Location { get; }
}
