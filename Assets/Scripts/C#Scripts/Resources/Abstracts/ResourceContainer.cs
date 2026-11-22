using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Base class for resource objects.
/// </summary>
[RequireComponent(typeof(ParticleSystem))]
public abstract class ResourceContainer : MonoBehaviour, IHarvestable
{
    [SerializeField]
    protected int charges;
    [SerializeField]
    float timeToHarvest;

    /// <summary>
    /// Triggers whenever the number of available resources changes.
    /// </summary>
    /// <remarks>This delegete is ideally used to update animations/models.</remarks>
    protected event Action<int> CountDecreased;

    public Transform Location { get => transform; }

    protected ResourceType resourceType = ResourceType.NULL;

    [SerializeField]
    ParticleSystem particleSystem;

    private void Start()
    {
        CountDecreased += (_) => ActivateDecreaseEffect();
    }

    public float GetBaseHarvestTime() => timeToHarvest;
    public ResourceType GetResourceType() => resourceType;
    public bool IsEmpty() => charges == 0;

    /// <summary>
    /// Removes a single resource charge
    /// </summary>
    public bool RemoveCharge()
    {
        int postWithdraw = charges - 1;
        if (postWithdraw < 0)
            return false;

        charges = postWithdraw;
        CountChangeNotif();
        return true;
    }

    public void Extract(Action<ResourceType> OnHarvestEnd)
    {
        Debug.Log("Starting Harvest.");

        if (IsEmpty())
            Debug.LogError("Trying to Harvest from an empty resource container.");

        StartCoroutine(Harvest(OnHarvestEnd));
    }

    public IEnumerator Harvest(Action<ResourceType> OnHarvestEnd)
    {
        yield return new WaitForSeconds(timeToHarvest);
        OnHarvestEnd.Invoke(resourceType);
        RemoveCharge();
    }

    protected void CountChangeNotif() => CountDecreased?.Invoke(charges);

    void ActivateDecreaseEffect() 
    {
        particleSystem.Play();
    }

}