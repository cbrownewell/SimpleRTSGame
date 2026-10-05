using System.Collections.Generic;
using System.Linq;
using UnitScripts.States.Worker;
using UnityEngine;

public class EntityLocator :MonoBehaviour
{
    private void Awake()
    {
        DepositState.FindDepot += LocateDepotEntrance;
        HarvestState.FindResource += LocateHarvestable;
    }

    #region Buildings
    /// <summary>
    /// Finds the closest resource drop off building.
    /// </summary>
    /// <param name="start"></param>
    /// <returns></returns>
    Transform LocateDepotEntrance(Vector3 start) 
    {
        var depot = GameObject.Find("CoreBase");
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
    IHarvestable LocateHarvestable(ResourceType type, Vector3 start) 
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

}
