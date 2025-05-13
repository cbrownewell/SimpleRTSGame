using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerUnitMono : MonoBehaviour, IMoveable<Vector3>, ISelectable
{
    WorkerUnit workerUnit;

    [SerializeField]
    SpriteRenderer selectCircle;

    NavMeshAgent agent;

    [SerializeField]
    float speed = 0;

    public void Highlight(bool on)
    {
        selectCircle.enabled = on;
    }

    public void MoveTo(Vector3 target)
    {
        agent.SetDestination(target);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        workerUnit = new WorkerUnit(agent, speed);
        agent.speed = speed;
    }

}
