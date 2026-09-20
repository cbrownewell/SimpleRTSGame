using UnityEngine;
using UnityEngine.AI;
using Commands.Components;
using Commands.Components.Abstract;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(NavMeshAgent))]
[RequireComponent(typeof(CommandQueue))]
public abstract class GameUnit : MonoBehaviour, IMoveable<Vector3>, ISelectable
{
    [SerializeField]
    protected float speed = 0;

    [SerializeField]
    SpriteRenderer selectCircle;

    [SerializeField]
    GameObject model;

    protected NavMeshAgent agent;
    protected ICommandQueue commandQueue;
    public NavMeshAgent NavMeshAgent { get { return agent; } }

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        commandQueue = GetComponent<CommandQueue>();
    }

    public void MoveTo(Vector3 target) => agent.SetDestination(target);
    public void Highlight(bool on) => selectCircle.enabled = on;
    public void ToggleModel(bool onOff) => model.SetActive(onOff);
}
