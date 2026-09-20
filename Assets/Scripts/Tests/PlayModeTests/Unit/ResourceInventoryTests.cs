using UnityEngine;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;


public class ResourceInventoryTests
{
    /// <summary>
    /// Ensures inventory maintains the resource type initially entered into it
    /// </summary>
    [UnityTest]
    public IEnumerator CheckResourceType_Success()
    {
        IResourceInventory inventory = new GameObject().AddComponent<ResourceInventory>();
        inventory.AddToInventory(ResourceType.WOOD);

        Assert.AreEqual(ResourceType.WOOD, inventory.PeekResourceType(), "Type entered into inventory should be the same upon retrieval");
        yield return null;
    }

    /// <summary>
    /// Ensures inventory can detect if it is full
    /// </summary>
    [UnityTest]
    public IEnumerator CheckInventoryFull_Success() 
    {
        IResourceInventory inventory = new GameObject().AddComponent<ResourceInventory>();
        inventory.AddToInventory(ResourceType.WOOD);

        Assert.True(inventory.InventoryFull(), "Inventory should be full");
        yield return null;
    }

    /// <summary>
    /// Ensures inventory can detect if it is not full
    /// </summary>
    [UnityTest]
    public IEnumerator CheckInventoryFull_Fail()
    {
        IResourceInventory inventory = new GameObject().AddComponent<ResourceInventory>();

        Assert.False(inventory.InventoryFull(), "Inventory should not be full");
        yield return null;
    }

    /// <summary>
    /// Checks to make sure a resource is removed from inventory properly after retrieval
    /// </summary>
    [UnityTest]
    public IEnumerator CheckInventoryEmptyAfterRetrieval() 
    {
        IResourceInventory inventory = new GameObject().AddComponent<ResourceInventory>();
        inventory.AddToInventory(ResourceType.WOOD);

        inventory.GetResourceFromInventory();

        Assert.False(inventory.InventoryFull(), "Inventory should be empty after retrieval.");

        yield return null;
    }

}
