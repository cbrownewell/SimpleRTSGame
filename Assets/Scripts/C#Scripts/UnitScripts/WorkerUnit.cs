using StateMachine.Abstracts;
using UnitScripts.StateMachines;
using UnitScripts.States.Worker;
using UnityEngine;

[RequireComponent(typeof(ResourceInventory))]
public class WorkerUnit : GameUnit
{
    public IResourceInventory resourceInventory;
    public IStateMachine stateMachine;

    public float reach;

    [SerializeField]
    private float awareness;

    protected override void Awake()
    {
        base.Awake();
        resourceInventory = GetComponent<IResourceInventory>();
        stateMachine = new WorkerStateMachine(new WorkerIdleState(this), this);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        stateMachine.Tick();
    }
}
