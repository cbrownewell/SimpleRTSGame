using System.Collections;
using UnityEngine;

public class HarvestCommand : Command
{
    int _harvestTime = 1;
    IHarvestable _harvestable;
    IResourceInventory _resourceInventory;
    GameUnit _unit;

    public override bool isFinished => _resourceInventory.InventoryFull();

    public HarvestCommand(IHarvestable harvestObj, IResourceInventory inventory, int harvestDuration, GameUnit unit)
    {
        _harvestable = harvestObj;
        _resourceInventory = inventory;
        _harvestTime = harvestDuration;
        _unit = unit;
    }

    public override void Execute()
    {
        if (!_harvestable.IsEmpty())
        {
            _unit.StartCoroutine(Harvest());
        }
    }

    IEnumerator Harvest() 
    {
        yield return new WaitForSeconds(_harvestTime);
        var resource = _harvestable.Extract();
        _resourceInventory.AddToInventory(resource);
    }

}
