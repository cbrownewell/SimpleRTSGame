using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitCommandBuilder : IUnitCommandBuilder
{
    List<Command> _commands = new List<Command>();
    public IUnitCommandBuilder AddMovementCommand(NavMeshAgent agent, Vector3 destination, GameObject target = null) 
    {
        if(target != null)
            _commands.Add(new MoveCommand(agent, destination, target));
        else
            _commands.Add(new MoveCommand(agent, destination));

        return this;
    }
    public IUnitCommandBuilder AddHarvestCommand(IHarvestable harvestObj, IResourceInventory inventory, int harvestDuration, GameUnit unit) 
    {
        _commands.Add(new HarvestCommand(harvestObj, inventory, harvestDuration, unit));
        return this;
    }
    public IUnitCommandBuilder AddStoreCommand(IResourceInventory inventory, GameUnit unit) 
    {
        _commands.Add(new StoreCommand(inventory, unit));
        return this;
    }
    public IUnitCommandBuilder AddLoopCommand(ICommandQueue queue) 
    {
        _commands.Add(new LoopCommand(queue, _commands.GetRange(0, _commands.Count)));
        return this;
    }
    public List<Command> Finish() { return _commands; } 
}
