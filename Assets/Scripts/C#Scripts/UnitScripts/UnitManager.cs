using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Commands.Components.Abstract;
using Commands.Components;

public class UnitManager : MonoBehaviour
{
    IUnitCommandBuilder commandBuilder = new UnitCommandBuilder();
    /// <summary>
    /// Assign actions to the select group of Units based on the raycast info
    /// </summary>
    /// <param name="hitInfo"></param>
    /// <param name="selectedUnits"></param>
    public void AssignActions(RaycastHit hitInfo, List<GameObject> selectedUnits) 
    {
        var listSample = selectedUnits.First();

        if (listSample.TryGetComponent<WorkerUnit>(out _))
        {
            IEnumerable<WorkerUnit> workers =
                selectedUnits.Where(x => x.TryGetComponent<WorkerUnit>(out _))
                .Select(j => j.GetComponent<WorkerUnit>());

            if (hitInfo.collider.gameObject.layer == 7) //If the raycast hit the ground
            {
                AssignMovementActions(hitInfo.point, workers);
            }
            else if (hitInfo.collider.gameObject.layer == 8) //If the raycast hit a resource/ the resource layer
            {
                AssignHarvestActions(hitInfo.collider, workers);
            }
        }
    }

    /// <summary>
    /// Creates and assigns all actions needed for a Unit to move somewhere
    /// </summary>
    /// <param name="point"></param>
    /// <param name="workers"></param>
    void AssignMovementActions(Vector3 point, IEnumerable<WorkerUnit> workers) 
    {
        var targetPoint = point;

        foreach (var item in workers)
        {
            var commandQueue = item.GetComponent<ICommandQueue>();

            var cmdSeq = commandBuilder.AddMovementCommand(item.NavMeshAgent, targetPoint).Finish();

            commandQueue.AddCommandToQueue(cmdSeq);
        }
    }

    /// <summary>
    /// Creates and assigns all necessary actions for a worker unit to harvest the target resource
    /// </summary>
    /// <param name="collider"></param>
    /// <param name="workers"></param>
    void AssignHarvestActions(Collider collider, IEnumerable<WorkerUnit> workers) 
    {
        //var baseObj = GameObject.Find("CoreBase").GetComponent<GameBuilding>();//This will need to be replaced eventually

        collider.TryGetComponent<IHarvestable>(out var harvest);

        if (harvest.IsEmpty() is true)
            return;

        foreach (var item in workers)
        {
            var commandQueue = item.GetComponent<ICommandQueue>();

            var cmdSeq = commandBuilder.AddMovementCommand(item.NavMeshAgent, collider.gameObject)
                .AddHarvestCommand(harvest, item.resourceInventory, item)
                .AddMovementCommand(item.NavMeshAgent, new Vector3())//baseObj.EntryPointCoords)
                .AddStoreCommand(item.resourceInventory, item)
                .AddLoopCommand(commandQueue)
                .Finish();

            commandQueue.AddCommandToQueue(cmdSeq);
        }

    }
}
