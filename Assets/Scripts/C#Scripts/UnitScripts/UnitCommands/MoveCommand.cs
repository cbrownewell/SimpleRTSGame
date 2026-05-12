using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class MoveCommand : Command
{
    NavMeshAgent _agent;
    Vector3 _destination;
    GameObject _target;

    public MoveCommand(NavMeshAgent agent, Vector3 destination, GameObject target = null)
    {
        _agent = agent;
        _destination = destination;
        _target = target;
    }

    public override bool isFinished => _agent.remainingDistance <= 1.0f;

    public override void Cancel()
    {
        throw new System.NotImplementedException();
    }

    public override void Execute()//Needs to look at a way to have the position get recalculated
    {
        if (_target != null)
        {
            //Math is wrong here somewhere.  Distance is being calculated wrong
            var direction = _agent.gameObject.transform.position - _target.transform.position;
            var rad = _target.gameObject.GetComponent<SphereCollider>().radius;

            var targetDestination = _target.transform.position + ((rad * direction.normalized));
            Debug.Log(targetDestination);
            //_agent.SetDestination(targetDestination);
        }
        else 
        {
            _agent.SetDestination(_destination);
        }
    }
}
