using System.Linq;
using UnityEngine;

/// <summary>
/// Service designed to locate objects in the game world.
/// </summary>
public class ObjectLocator : MonoBehaviour
{

    void Start()
    {
        
    }

    /// <summary>
    /// Locates the nearest harvestable of the specified type
    /// </summary>
    /// <param name="position">Origin point of the search.</param>
    /// <param name="type">Type of resource to be located.</param>
    /// <returns></returns>
    public IHarvestable LocateNearestResource(Vector3 position, ResourceType type) 
    {
        var colliders = Physics.OverlapSphere(position, 10, 8);
        var validNodes = colliders.Where(n => n.TryGetComponent<IHarvestable>(out _))
            .Select(n => n.GetComponent<Transform>())
            .Where(n => n.GetComponent<IHarvestable>().GetResourceType() == type);

        //If there are no nodes, exit and cancel the loop
        if (!validNodes.Any())
            return null;

        //If there is only one node use that
        if (validNodes.Count() is 1)
            return validNodes.First().GetComponent<IHarvestable>();

        //Cycle through and find the 
        float closest = float.MaxValue;
        var closestNode = validNodes.First();
        foreach (var node in validNodes)
        {
            var dist = Vector3.Distance(position, node.position);
            if (dist < closest)
            {
                closestNode = node;
                closest = dist;
            }
        }

        return closestNode.GetComponent<IHarvestable>();
    }
}
