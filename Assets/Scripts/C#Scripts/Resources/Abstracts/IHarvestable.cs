using System;
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
    /// Starts the Extraction Process
    /// </summary>
    /// <returns>Type of resource extracted</returns>
    void Extract(Action<ResourceType> OnHarvestEnd);
    bool IsEmpty();
    ResourceType GetResourceType();
    float GetBaseHarvestTime();

    public Transform Location { get; }
}
