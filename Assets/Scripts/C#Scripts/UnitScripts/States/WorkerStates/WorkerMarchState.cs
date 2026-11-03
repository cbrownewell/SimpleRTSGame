using StateMachine.States.Abstracts;
using System.Collections;
using UnityEngine;

namespace UnitScripts.States.Worker 
{
    public class WorkerMarchState : BaseWorkerState
    {
        IState previous;
        Transform location;

        private float remainingDistance 
        {
            get
            {
                var remaining = Vector3.Distance(worker.transform.position, location.position);
                return remaining;
            }
        }


        public bool Arrived { 
            get 
            {
                var adjusted = remainingDistance;
                if (adjusted <= worker.Reach) 
                    return true;
                else
                    return false;
            }
        }
       
        public WorkerMarchState(WorkerUnit owner, Transform target, IState requestor = null) : base(owner)
        {
            previous = requestor;
            location = target;
        }
        public override void InitState()
        {
            worker.NavMeshAgent.SetDestination(location.position);//This could prove to have some bad pathing (maybe offload to a Navmesh path generator?
            worker.NavMeshAgent.isStopped = false;
            worker.StartCoroutine(TrackMarch());
        }

        public override IState ExecuteState()
        {
            //Debug.Log("Distance left is " + (remainingDistance - worker.Reach));
            //Debug.Log("Adjusted distance left is " + (Mathf.Round(remainingDistance) - worker.Reach));
            //If we've reached the destination
            if (worker.NavMeshAgent.isStopped is true)
            {
                //If this request didn't come from within another request then we're done.
                if (previous is null)
                    return new WorkerIdleState(worker);
                else
                    return previous;
            }
            return this;
        }

        public override void CloseState()
        {
            worker.StopCoroutine(TrackMarch());
        }

        IEnumerator TrackMarch()
        {
            yield return new WaitUntil(() => Arrived);
            worker.NavMeshAgent.isStopped = true;
            worker.NavMeshAgent.velocity = Vector3.zero;
        }
    }
}