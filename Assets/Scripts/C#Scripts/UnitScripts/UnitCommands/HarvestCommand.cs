using System;
using System.Collections;
using UnityEngine;

public class HarvestCommand : Command
{
    float _harvestTime;
    IHarvestable _harvestable;
    IResourceInventory _resourceInventory;
    GameUnit _unit;

    public event Func<Vector3, ResourceType, IHarvestable> GetNewHarvestable;

    public override bool IsFinished => _resourceInventory.InventoryFull();

    public HarvestCommand(IHarvestable harvestObj, IResourceInventory inventory, GameUnit unit)
    {
        _harvestable = harvestObj;
        _resourceInventory = inventory;
        _harvestTime = harvestObj.GetBaseHarvestTime();
        _unit = unit;
    }

    public override void Execute()
    {
        if (_harvestable.IsEmpty() is false)
            _unit.StartCoroutine(Harvest());
        else 
        {
            FindNewResource();
        }
    }

    /// <summary>
    /// Calls locator service to find the closest resource node that can be harvested from.
    /// </summary>
    /// <returns></returns>
    IHarvestable FindNewResource() 
    {
        return GetNewHarvestable?.Invoke(_unit.gameObject.transform.position, _harvestable.GetResourceType());
    }

    IEnumerator Harvest()
    {
        if (_harvestable.IsEmpty() || _harvestable.GetResourceType() is ResourceType.NULL) 
        {
            Cancel();
            yield return null;
        }

        yield return new WaitForSeconds(_harvestTime);
        var resource = _harvestable.Extract();
        _resourceInventory.AddToInventory(resource);
    }

    public override void Cancel()
    {
        _unit.StopCoroutine(Harvest());
        base.Cancel();
    }
}
