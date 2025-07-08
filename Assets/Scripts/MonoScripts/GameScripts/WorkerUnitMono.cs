using System.Collections;
using UnityEngine;

public class WorkerUnitMono : GameUnit, IResourceInventory
{
    WorkerUnit workerUnit;

    Inventory resourceInventory;

    private void Start()
    {
        workerUnit = new WorkerUnit(_agent, speed);
        resourceInventory = new Inventory();
        _agent.speed = speed;
    }

    private void Update()
    {
        ProcessCommands();
    }

    public void AddToInventory(ResourcePackage obj)
    {
        var added = resourceInventory.AddToInventory(obj);
    }

    public void GetAllFromInventory()
    {
        resourceInventory.RetrieveAllInventory();
    }

    ResourceType IResourceInventory.PeekInventoryType()
    {
        return resourceInventory.PeekType();
    }

    public bool InventoryFull()
    {
        return resourceInventory.InventoryFull;
    }
}
