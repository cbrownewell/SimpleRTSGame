using UnityEngine;

/// <summary>
/// Base class for resource objects that can regenerate their resources.
/// </summary>
public abstract class RegenContainer : ResourceContainer, IResourceRegen
{
    [SerializeField]
    int chargeRegen = 0;

    [SerializeField]
    int maxCharges = 0;

    /// <summary>
    /// Removes a single charge
    /// </summary>
    public void AddCharges(int value)
    {
        int postAdd = charges + value;
        if (postAdd > maxCharges)
            charges = maxCharges;
        else
            charges = postAdd;

        CountChangeNotif();
    }

    /// <summary>
    /// Restore resources
    /// </summary>
    public void RegenResources()
    {
        if (charges == maxCharges)
            return;
        else
            AddCharges(chargeRegen);
    }


}
