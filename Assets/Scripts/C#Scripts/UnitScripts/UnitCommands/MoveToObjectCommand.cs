using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Command used when navigating to a specific object.
/// </summary>
public class MoveToObjectCommand : Command
{
    NavMeshAgent _agent;
    GameObject _target;

    public MoveToObjectCommand(NavMeshAgent agent, GameObject target)
    {
        _agent = agent;
        _target = target;
    }

    public override bool IsFinished => _agent.remainingDistance <= 1.0f;

    public override void Cancel()
    {
        _agent.SetDestination(_agent.gameObject.transform.position);
    }

    public override void Execute()//Needs to look at a way to have the position get recalculated
    {
        //Math is wrong here somewhere.  Distance is being calculated wrong
        var direction = _agent.gameObject.transform.position - _target.transform.position;
        var rad = _target.gameObject.GetComponent<SphereCollider>().radius;

        var targetDestination = _target.transform.position + ((rad * direction.normalized));
        Debug.Log(targetDestination);
        _agent.SetDestination(targetDestination);
    }
}
