using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Commands.Components.Abstract 
{
    public interface IUnitCommandBuilder
    {
        public IUnitCommandBuilder AddMovementCommand(NavMeshAgent agent, GameObject target);
        public IUnitCommandBuilder AddMovementCommand(NavMeshAgent agent, Vector3 destination);
        public IUnitCommandBuilder AddHarvestCommand(IHarvestable harvestObj, IResourceInventory inventory, MonoBehaviour unit);
        public IUnitCommandBuilder AddStoreCommand(IResourceInventory inventory, MonoBehaviour unit);
        public IUnitCommandBuilder AddLoopCommand(ICommandQueue queue);
        public List<Command> Finish();
    }
}


