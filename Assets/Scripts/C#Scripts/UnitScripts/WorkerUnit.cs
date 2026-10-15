using StateMachine.Abstracts;
using UnitScripts.StateMachines;
using UnitScripts.States.Worker;
using UnityEngine;

[RequireComponent(typeof(ResourceInventory))]
public class WorkerUnit : GameUnit
{
    public IResourceInventory resourceInventory;
    public IStateMachine stateMachine;

    [SerializeField]
    private float reach = 0.1f;
    public float Reach { get { return reach; } }

    [SerializeField]
    private float awareness;
    public float Awareness { get { return awareness; } }

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
