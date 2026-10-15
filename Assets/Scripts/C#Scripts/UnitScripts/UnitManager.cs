using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnitScripts.States.Worker;

public class UnitManager : MonoBehaviour
{
    /// <summary>
    /// Assign actions to the select group of Units based on the raycast info
    /// </summary>
    /// <param name="hitInfo"></param>
    /// <param name="selectedUnits"></param>
    public void AssignActions(RaycastHit hitInfo, List<GameObject> selectedUnits) 
    {
        var listSample = selectedUnits.First();

        if (listSample.TryGetComponent<WorkerUnit>(out _))//So far we can only select a group of one type of Unit
        {
            IEnumerable<WorkerUnit> workers =
                selectedUnits.Where(x => x.TryGetComponent<WorkerUnit>(out _))
                .Select(j => j.GetComponent<WorkerUnit>());

            if (hitInfo.collider.gameObject.layer == 7) //If the raycast hit the ground
            {
                foreach (var worker in workers)
                {
                    PushMovementStatePoint(hitInfo.point, workers);
                }
            }
            else if (hitInfo.collider.gameObject.layer == 8) //If the raycast hit a resource/ the resource layer
            {
                hitInfo.collider.TryGetComponent<IHarvestable>(out var harvest);

                if (harvest.IsEmpty() is true)
                    return;

                foreach (var worker in workers)
                { 
                    PushHarvestState(harvest, worker);
                }
            }
        }
    }

    /// <summary>
    /// Push a movement state with priority.
    /// </summary>
    /// <param name="point"></param>
    /// <param name="workers"></param>
    /// <remarks>We pass in a Vec3 point, because passing in the ground transform is not viable.</remarks>
    void PushMovementStatePoint(Vector3 point, IEnumerable<WorkerUnit> workers) 
    {
        //Since the point does not have a transform, create an empty gameobject
        var movePoint = new GameObject().transform;
        movePoint.position = point;

        foreach (var worker in workers)
        {
            worker.stateMachine.PushPriorityState(new WorkerMarchState(worker, movePoint, worker.Reach));
        }
    }

    /// <summary>
    /// Pushes a harvest state to a worker with priority.
    /// </summary>
    /// <param name="harvest"></param>
    /// <param name="worker"></param>
    void PushHarvestState(IHarvestable harvest, WorkerUnit worker) 
    {
       worker.stateMachine.PushPriorityState(new HarvestState(worker, harvest));   
    }
}
