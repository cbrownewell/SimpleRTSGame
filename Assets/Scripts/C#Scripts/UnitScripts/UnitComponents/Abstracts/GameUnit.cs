using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(NavMeshAgent))]
public abstract class GameUnit : MonoBehaviour, IMoveable<Vector3>, ISelectable
{
    [SerializeField]
    protected float speed = 0;

    [SerializeField]
    SpriteRenderer selectCircle;

    [SerializeField]
    GameObject model;

    [SerializeField]
    private float reach = 0.1f;

    public float Reach { get { return reach; } }

    protected NavMeshAgent agent;
    public NavMeshAgent NavMeshAgent { get { return agent; } }

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = reach;
    }

    public void MoveTo(Vector3 target) => agent.SetDestination(target);
    public void Highlight(bool on) => selectCircle.enabled = on;
    public void ToggleModel(bool onOff) => model.SetActive(onOff);

    private void OnDrawGizmos()
    {
        // Set the color with custom alpha
        Gizmos.color = Color.red; // Red with custom alpha

        // Draw the sphere
        Gizmos.DrawWireSphere(transform.position, reach);
    }

}
