using StateMachine.States.Abstracts;
using System;
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
        private readonly float marchLength;

        /// <summary>
        /// How how longer in percentage the worker has until it reaches its destination.
        /// </summary>
        /// <remarks>0-1 scale. 0 means destination has been reached.</remarks>
        public float PercentLeft { 
            get {
                var dist = Vector3.Distance(worker.transform.position, location.position);//Get distance left
                var distMod = dist - stopDist;
                var rawPercent = distMod / marchLength;//Raw percent by taking the distance left, subtracting the unit's reach, and then dividing my the total journey length
                var percent = (float)Math.Round(rawPercent, 2, MidpointRounding.ToEven);//Make proper precentage
                return percent;//Dividing sucks so it might never reach 0 thus we round down at a certain point
            }
        }
       
        public WorkerMarchState(WorkerUnit owner, Transform transform, float distance, IState requestor = null) : base(owner)
        {
            previous = requestor;
            stopDist = distance;
            location = transform;
            var val = Vector3.Distance(worker.transform.position, location.position);

            //March Length cannot be 0
            if (val > 0)
                marchLength = val;
            else
                throw new ArgumentOutOfRangeException("marchLength cannot be or be less than zero. Was: " + val);
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
            while (PercentLeft > 0)
                yield return null;

            marching = false;
        }
    }
}