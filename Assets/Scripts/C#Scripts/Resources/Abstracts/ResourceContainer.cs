using System;
using UnityEngine;

/// <summary>
/// Base class for resource objects.
/// </summary>
public abstract class ResourceContainer : MonoBehaviour, IHarvestable
{
    [SerializeField]
    protected int maxCharges;
    [SerializeField]
    int currentCount = 0;
    [SerializeField]
    float timeToHarvest = 1;

    /// <summary>
    /// Triggers whenever the number of available resources changes.
    /// </summary>
    /// <remarks>This delegete is ideally used to update animations/models.</remarks>
    protected event Action<int> CountChanged;

    public int chargesLeft => chargeCount;

    public Transform Location { get => transform; }

    protected int chargeCount;

    protected ResourceType resourceType = ResourceType.NULL;

    private void Start()
    {
        chargeCount = maxCharges;
    }

    private void Update()
    {
        currentCount = chargeCount;
    }

    public float GetBaseHarvestTime() => timeToHarvest;
    public ResourceType GetResourceType() => resourceType;
    public bool IsEmpty() => chargeCount == 0;

    /// <summary>
    /// Removes a single resource charge
    /// </summary>
    public bool RemoveCharge()
    {
        int postWithdraw = chargeCount - 1;
        if (postWithdraw < 0)
            return false;

        chargeCount = postWithdraw;
        CountChangeNotif();
        return true;
    }

    public ResourceType Extract()
    {
        if (IsEmpty())
            return ResourceType.NULL;

        RemoveCharge();
        return resourceType;
    }

    protected void CountChangeNotif() => CountChanged?.Invoke(chargeCount);
    


}
