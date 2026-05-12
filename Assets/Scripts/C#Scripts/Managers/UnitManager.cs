using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
                var targetPoint = hitInfo.point;

                foreach (var item in workers)
                {
                    var commandQueue = item.GetComponent<ICommandQueue>();
                    commandQueue.AddCommand(new MoveCommand(item.NavMeshAgent, targetPoint));
                }
            }
            else if (hitInfo.collider.gameObject.layer == 8) //If the raycast hit a resource
            {
                var targetPoint = hitInfo.point;
                
                var baseObj = GameObject.Find("CoreBase").GetComponent<GameBuilding>();//This will need to be replaced eventually

                foreach (var item in workers)
                {
                    hitInfo.collider.TryGetComponent<IHarvestable>(out var harvest);

                    var commandQueue = item.GetComponent<ICommandQueue>();

                    var cmdSeq = commandBuilder.AddMovementCommand(item.NavMeshAgent, targetPoint, hitInfo.collider.gameObject)
                        .AddHarvestCommand(harvest, item.resourceInventory, 1, item)
                        .AddMovementCommand(item.NavMeshAgent, baseObj.EntryPointCoords)
                        .AddStoreCommand(item.resourceInventory, item)
                        .AddLoopCommand(commandQueue)
                        .Finish();

                    commandQueue.AddCommandToQueue(cmdSeq);
                }
            }
        }
    }
}
