using UnityEngine;

public abstract class ResourceContainer: MonoBehaviour, IHarvestable
{
    [SerializeField]
    int maxCharges = 5;
    [SerializeField]
    int chargeRegen = 0;
    [SerializeField]
    int currentCount = 0;

    protected delegate void ResourceCountUpdated(int newCount);
    /// <summary>
    /// Triggers whenever the number of available resources changes.
    /// </summary>
    /// <remarks>This delegete is ideally used to update animations/models.</remarks>
    protected event ResourceCountUpdated CountChanged;

    public int chargesLeft => chargeCount;
    int chargeCount;
    
    public ResourceType resourceType = ResourceType.NULL;

    private void Start()
    {
        chargeCount = maxCharges;
    }

    private void Update()
    {
        currentCount = chargeCount;
    }

    /// <summary>
    /// Changes the amount stored in the resource
    /// </summary>
    /// <param name="val">value of change</param>
    /// <param name="subtract">whether we are subtracting or adding</param>
    /// <returns>true or false based on whether the change was possible</returns>
    private bool ChangeAmount(int val = 1, bool subtract = false)
    {
        if (subtract)
        {
            int postWithdraw = chargeCount - val;
            if (postWithdraw < 0)
                return false;
            chargeCount = postWithdraw;
        }
        else 
        {
            int postRegen = chargeCount + val;
            if (postRegen > maxCharges)
                return false;
            chargeCount = postRegen;
        }
        CountChanged?.Invoke(chargeCount);
        return true;
    }

    /// <summary>
    /// Restore resources
    /// </summary>
    public void RegenResources() 
    {
        ChangeAmount(chargeRegen, false);
    }

    public ResourceType Extract()
    {
        if (IsEmpty() == true)
            return ResourceType.NULL;

        ChangeAmount(1, true);
        return resourceType;
    }

    public ResourceType GetResourceType() => resourceType;
    public bool IsEmpty() => chargeCount == 0;
    
}
