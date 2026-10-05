using StateMachine.States.Abstracts;
using System.Collections;
using UnityEngine;

namespace UnitScripts.States.Worker 
{
    public class WorkerMarchState : BaseWorkerState
    {
        IState previous;
        Transform location;
        float stopDist;
        bool marching = true;

        public WorkerMarchState(WorkerUnit owner, Transform transform, float distance) : base(owner)
        {
            location = transform;
            stopDist = distance;
        }

        /// <summary>
        /// Use this constrcutor when making a march request from another state.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="transform"></param>
        /// <param name="requestor"></param>
        public WorkerMarchState(WorkerUnit owner, Transform transform, float distance, IState requestor) : base(owner)
        {
            previous = requestor;
            stopDist = distance;
            location = transform;
        }
        public override void InitState()
        {
            worker.NavMeshAgent.SetDestination(location.position);//This could prove to have some bad pathing (maybe offload to a Navmesh path generator?
            worker.StartCoroutine(TrackMarch());
        }

        public override IState ExecuteState()
        {
            //If we've reached the destination
            if (marching is false)
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
            var diff = Vector3.Distance(worker.transform.position, location.position);
            while (diff <= stopDist)
                yield return null;

            marching = false;
        }
    }
}