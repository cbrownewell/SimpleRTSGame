using UnityEngine;

[RequireComponent(typeof(ResourceInventory))]
public class WorkerUnit : GameUnit
{
    public IResourceInventory resourceInventory;
    [SerializeField]
    private float awareness;

    protected override void Awake()
    {
        base.Awake();
        resourceInventory = GetComponent<IResourceInventory>();
    }

    private void Start()
    {
        
    }
}
