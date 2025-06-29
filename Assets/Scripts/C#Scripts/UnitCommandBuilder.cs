using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitCommandBuilder
{
    List<Command> _commands = new List<Command>();
    public UnitCommandBuilder AddMovementCommand(NavMeshAgent agent, Vector3 destination) 
    {
        _commands.Add(new MoveCommand(agent, destination));
        return this;
    }
    public UnitCommandBuilder AddHarvestCommand(IHarvestable harvestObj, IResourceInventory inventory, int harvestDuration, GameUnit unit) 
    {
        _commands.Add(new HarvestCommand(harvestObj, inventory, harvestDuration, unit));
        return this;
    }
    public UnitCommandBuilder AddStoreCommand(IResourceInventory inventory, GameUnit unit) 
    {
        _commands.Add(new StoreCommand(inventory, unit));
        return this;
    }
    public UnitCommandBuilder AddLoopCommand(ICommandQueue queue) 
    {
        _commands.Add(new LoopCommand(queue, _commands.GetRange(0, _commands.Count)));
        return this;
    }
    public List<Command> Finish() { return _commands; } 
}
