using System.Linq;
using UnityEngine;

/// <summary>
/// Service designed to locate resources in the game world.
/// </summary>
public class ResourceLocator
{
    /// <summary>
    /// Locates the nearest resource with the specified type that is not empty.
    /// </summary>
    /// <param name="startPosition">Origin point of the search.</param>
    /// <param name="type">Resource type of harvestable to find.</param>
    /// <param name="range">How far to look for another resource (radius).</param>
    /// <remarks>Will search entire gamespace if range is not defined.</remarks>
    /// <returns></returns>
    public static IHarvestable LocateNearestResource(Vector3 startPosition, ResourceType type, float range = float.MaxValue) 
    {
        Collider[] colliders;
        colliders = Physics.OverlapSphere(startPosition, 10, 8);
        var validNodes = colliders.Where(n => n.TryGetComponent<IHarvestable>(out _))
            .Select(n => n.GetComponent<Transform>())
            .Where(n => n.GetComponent<IHarvestable>().GetResourceType() == type
            && n.GetComponent<IHarvestable>().IsEmpty() is false);
            

        //If there are no nodes, exit and cancel the loop
        if (!validNodes.Any())
            return null;

        //If there is only one node use that
        if (validNodes.Count() is 1)
            return validNodes.First().GetComponent<IHarvestable>();

        //Cycle through and find the closest
        float closest = float.MaxValue;
        var closestNode = validNodes.First();
        foreach (var node in validNodes)
        {
            var dist = Vector3.Distance(startPosition, node.position);
            if (dist < closest)
            {
                closestNode = node;
                closest = dist;
            }
        }

        return closestNode.GetComponent<IHarvestable>();
    }
}



