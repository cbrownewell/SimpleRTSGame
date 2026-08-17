using System.Collections;
using UnityEngine;

/// <summary>
/// Removes all items from a resource inventory and deposits them
/// </summary>
public class StoreCommand : Command
{
    public override bool IsFinished => !resourceInventory.InventoryFull();

    IResourceInventory resourceInventory;
    GameUnit unit;

    public StoreCommand(IResourceInventory inventory, GameUnit unit) 
    {
        resourceInventory = inventory;
        this.unit = unit;
    }


    public override void Execute()
    {
        unit.StartCoroutine(StoreResources());
    }

    IEnumerator StoreResources() 
    {
        yield return new WaitForSeconds(1);
        resourceInventory.GetResourceFromInventory();
    }

    public override void Cancel()
    {
        unit.StopCoroutine(StoreResources());
    }
}
