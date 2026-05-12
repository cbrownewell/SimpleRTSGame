using System.Collections;
using UnityEngine;

public class StoreCommand : Command
{
    public override bool isFinished => !_resourceInventory.InventoryFull();

    IResourceInventory _resourceInventory;
    GameUnit _unit;

    public StoreCommand(IResourceInventory inventory, GameUnit unit) 
    {
        _resourceInventory = inventory;
        _unit = unit;
    }


    public override void Execute()
    {
        _unit.StartCoroutine(StoreResources());
    }

    IEnumerator StoreResources() 
    {
        yield return new WaitForSeconds(1);
        _resourceInventory.GetResourceFromInventory();
    }

    public override void Cancel()
    {
        _unit.StopCoroutine(StoreResources());
    }
}
