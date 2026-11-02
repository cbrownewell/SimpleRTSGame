using System.Collections.Generic;
using System.Linq;
using UnitScripts.States.Worker;
using UnityEngine;

public class EntityLocator : MonoBehaviour
{
    private void Awake()
    {
        DepositState.FindDepot += LocateDepotEntrance;
        HarvestState.FindResource += LocateHarvestable;
        FleeState.FindSafePoint += FindSafePoint;
    }

    #region Buildings
    /// <summary>
    /// Finds the closest resource drop off building.
    /// </summary>
    /// <param name="start"></param>
    /// <returns></returns>
    public Transform LocateDepotEntrance(Vector3 start) 
    {
        var depot = GameObject.FindWithTag("Deposit");
        depot.TryGetComponent<CoreBase>(out var building);
        return building.entryPoint;
    }

    #endregion


    #region Resources
    /// <summary>
    /// Finds the closest object in range of the start point with the IHarvestable interface.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="start"></param>
    /// <returns></returns>
    public IHarvestable LocateHarvestable(ResourceType type, Vector3 start) 
    {
        //Check surrounding area for resource nodes
        var colliders = Physics.OverlapSphere(start, 10f, LayerMask.GetMask("Resource"));

        //Filter down resource nodes to those that are relevant
        IEnumerable<IHarvestable> resources =
                colliders.Where(x => x.GetComponent<ResourceContainer>().GetResourceType() == type)
                .Where(x => x.GetComponent<ResourceContainer>().IsEmpty() is false)
                .Select(j => j.GetComponent<IHarvestable>());

        if (resources.Any() is false)
            return null;

        if (resources.Count() == 1)
            return resources.First();

        //Find the closest
        IHarvestable closest = resources.First();
        float distance = float.MaxValue;

        foreach (var resource in resources) 
        {
            var l = Vector3.Distance(start, resource.Location.position);
            if ( l < distance)
            {
                closest = resource;
                distance = l;
            }
        }
        return closest;
    }

    #endregion

    /// <summary>
    /// Finds a random point for a worker unit to run away to.
    /// </summary>
    /// <remarks>This method will not live here long term.</remarks>
    /// <param name="origin"></param>
    /// <returns></returns>
    public Transform FindSafePoint(Vector3 origin, float distance) 
    {
        var safePoint = new GameObject();
        var awayPoint = Random.onUnitCircle * distance; //This logic will have to be more complex later to ensure unit runs away from damage source.

        safePoint.transform.position = origin + new Vector3(awayPoint.x, 0f, awayPoint.y);
        return safePoint.transform;
    }


}
