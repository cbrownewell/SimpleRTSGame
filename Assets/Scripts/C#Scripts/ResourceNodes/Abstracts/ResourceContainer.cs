using UnityEngine;

public abstract class ResourceContainer: MonoBehaviour, IHarvestable
{
    public int maxAmount = 5;
    public int regenAmount = 0;

    public int resourcesLeft => currentCount;
    int currentCount;
    
    public ResourceType resourceType = ResourceType.NULL;

    private void Start()
    {
        InitContainer();
    }

    public void InitContainer() 
    {
        currentCount = maxAmount;
    }

    /// <summary>
    /// Changes the amount stored in the resource
    /// </summary>
    /// <param name="val">value of change</param>
    /// <param name="subtract">whether we are subtracting or adding</param>
    /// <returns>if -1 is returned then something went wrong</returns>
    private int ChangeAmount(int val = 1, bool subtract = false)
    {
        if (subtract)
        {
            int postWithdraw = currentCount - val;
            if (postWithdraw < 0)
                return -1;//returning -1 because it should never be negative
            currentCount = postWithdraw;
        }
        else 
        {
            int postRegen = currentCount + val;
            if (postRegen > maxAmount)
                return -1;
            currentCount = postRegen;
        }

       return val;
    }

    /// <summary>
    /// Restore resources
    /// </summary>
    public void RegenResources() 
    {
        ChangeAmount(regenAmount, false);
    }

    public ResourceType Extract()
    {
        if (IsEmpty() == true)
            return ResourceType.NULL;

        ChangeAmount(1, true);
        return resourceType;
    }

    public ResourceType GetResourceType() => resourceType;
    public bool IsEmpty() => currentCount == 0;
    
}
