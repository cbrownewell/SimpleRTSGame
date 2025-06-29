using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    NavMeshAgent _agent;
    Vector3 _destination;

    public MoveCommand(NavMeshAgent agent, Vector3 destination) 
    {
        _agent = agent;
        _destination = destination;
    }

    public override bool isFinished => _agent.remainingDistance <= 1.0f;

    public override void Execute()
    {
        var success = _agent.SetDestination(_destination);
        if (!success) //Retry
        {
            Debug.Log(success);
            NavMesh.SamplePosition(_destination, out var newHit, _agent.height * 2, NavMesh.AllAreas);
            
            _agent.SetDestination(_destination);
        }
    }
}
