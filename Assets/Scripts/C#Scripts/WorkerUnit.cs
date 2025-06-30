using UnityEngine;
using UnityEngine.AI;

public class WorkerUnit
{
    NavMeshAgent _agent;

    float _speed;
    //readonly float reach = 1;

    public WorkerUnit(NavMeshAgent agent, float speed) 
    {
        _agent = agent;
        _agent.speed = _speed = speed;        
    }




}
