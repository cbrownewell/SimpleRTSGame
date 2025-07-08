using UnityEngine;

public abstract class ResourceNode : MonoBehaviour, IHarvestable
{
    protected ResourceContainer container;

    [SerializeField]
    protected ResourceType resourceType;
    [SerializeField]
    protected int totalAmount;
    
    protected delegate void PostHarvest(int amount);
    protected PostHarvest postHarvest; //Should be an event instead?

    public bool IsEmpty() => container.IsEmpty;
    ResourcePackage IHarvestable.Extract(int extractAmount)
    {
        var returned = container.ExtractResources(extractAmount);
        postHarvest?.Invoke(extractAmount);
        return returned;
    }
}
