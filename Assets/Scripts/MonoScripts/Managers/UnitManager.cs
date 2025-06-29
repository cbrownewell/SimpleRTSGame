using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    UnitCommandBuilder commandBuilder = new UnitCommandBuilder();

    public void AssignActions(RaycastHit hitInfo, List<GameObject> selectedUnits) 
    {
        var listSample = selectedUnits.First();

        if (listSample.TryGetComponent<WorkerUnitMono>(out var worker))
        {
            IEnumerable<WorkerUnitMono> workers =
                selectedUnits.Where(x => x.TryGetComponent<WorkerUnitMono>(out _))
                .Select(j => j.GetComponent<WorkerUnitMono>());

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

                var baseObj = GameObject.Find("CoreBase").GetComponent<GameBuilding>();//This will need to be replaced

                foreach (var item in workers)
                {
                    hitInfo.collider.TryGetComponent<IHarvestable>(out var harvest);
                    item.TryGetComponent<IResourceInventory>(out var inventory);

                    var commandQueue = item.GetComponent<ICommandQueue>();

                    var cmdSeq = commandBuilder.AddMovementCommand(item.NavMeshAgent, targetPoint)
                        .AddHarvestCommand(harvest, inventory, 1, item)
                        .AddMovementCommand(item.NavMeshAgent, baseObj.EntryPointCoords)
                        .AddStoreCommand(inventory, item)
                        .AddLoopCommand(commandQueue)
                        .Finish();

                    commandQueue.AddCommandToQueue(cmdSeq);
                }
            }

        }
        else 
        {
        
        }

    }





}
