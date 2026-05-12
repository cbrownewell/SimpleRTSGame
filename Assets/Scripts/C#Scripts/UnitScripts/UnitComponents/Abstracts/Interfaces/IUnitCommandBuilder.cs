using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.AI;

public interface IUnitCommandBuilder
{
    public IUnitCommandBuilder AddMovementCommand(NavMeshAgent agent, Vector3 destination, GameObject target = null);
    public IUnitCommandBuilder AddHarvestCommand(IHarvestable harvestObj, IResourceInventory inventory, int harvestDuration, GameUnit unit);
    public IUnitCommandBuilder AddStoreCommand(IResourceInventory inventory, GameUnit unit);
    public IUnitCommandBuilder AddLoopCommand(ICommandQueue queue);
    public List<Command> Finish();
}
