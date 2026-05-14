using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Command used when navigating to a specific point.
/// </summary>
public class MoveToPointCommand : Command
{
    NavMeshAgent _agent;
    Vector3 _position;

    public MoveToPointCommand(NavMeshAgent agent, Vector3 destination)
    {
        _agent = agent;
        _position = destination;
    }

    public override bool IsFinished => _agent.remainingDistance <= 1.0f;

    public override void Cancel()
    {
        _agent.SetDestination(_agent.gameObject.transform.position);
    }

    public override void Execute()
    {
        _agent.SetDestination(_position);
    }
}
