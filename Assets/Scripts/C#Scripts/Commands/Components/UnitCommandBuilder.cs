using Commands.Components.Abstract;
using Commands.Unit;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Commands.Components 
{
    public class UnitCommandBuilder : IUnitCommandBuilder
    {
        List<Command> _commands = new();
        public IUnitCommandBuilder AddMovementCommand(NavMeshAgent agent, Vector3 destination)
        {
            _commands.Add(new MoveToPointCommand(agent, destination));
            return this;
        }

        public IUnitCommandBuilder AddMovementCommand(NavMeshAgent agent, GameObject target)
        {
            _commands.Add(new MoveToObjectCommand(agent, target));
            return this;
        }
        public IUnitCommandBuilder AddHarvestCommand(IHarvestable harvestObj, IResourceInventory inventory, MonoBehaviour unit)
        {
            var cmd = new HarvestCommand(harvestObj, inventory, unit);
            cmd.GetNewHarvestable += ResourceLocator.LocateNearestResource;
            _commands.Add(cmd);
            return this;
        }
        public IUnitCommandBuilder AddStoreCommand(IResourceInventory inventory, MonoBehaviour unit)
        {
            _commands.Add(new StoreCommand(inventory, unit));
            return this;
        }
        public IUnitCommandBuilder AddLoopCommand(ICommandQueue queue)
        {
            _commands.Add(new RepeatCommand(queue, _commands.GetRange(0, _commands.Count)));
            return this;
        }
        public List<Command> Finish()
        {
            List<Command> finished = new(_commands);
            _commands.Clear();
            return finished;
        }
    }
}


