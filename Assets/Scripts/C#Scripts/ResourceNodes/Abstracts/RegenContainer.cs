using UnityEngine;

/// <summary>
/// Base class for resource objects that can regenerate their resources.
/// </summary>
public abstract class RegenContainer : ResourceContainer, IResourceRegen
{
    [SerializeField]
    int chargeRegen = 0;

    /// <summary>
    /// Removes a single charge
    /// </summary>
    public void AddCharges(int value)
    {
        int postAdd = chargeCount + value;
        if (postAdd > maxCharges)
            chargeCount = maxCharges;
        else
            chargeCount = postAdd;

        CountChangeNotif();
    }

    /// <summary>
    /// Restore resources
    /// </summary>
    public void RegenResources()
    {
        if (chargeCount == maxCharges)
            return;
        else
            AddCharges(chargeRegen);
    }


}
