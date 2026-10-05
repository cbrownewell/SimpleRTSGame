using StateMachine.States;
using StateMachine.States.Abstracts;
using System;
using System.Collections;
using UnityEngine;

namespace UnitScripts.States.Worker 
{
    public class FleeState : BaseWorkerState
    {
        public static event Func<Vector3, Transform> FindSafePoint;

        Transform safePoint;

        float waitTime;

        bool hunkering = false;
        bool safe = false;

        public FleeState(WorkerUnit owner) : base(owner)
        {
        }

        public override void InitState()
        {
            //Find a safe distance away - Event not implmented yet
            safePoint = FindSafePoint.Invoke(worker.transform.position);
        }

        public override IState ExecuteState()
        {
            if (Vector3.Distance(worker.transform.position, safePoint.position) >= 1f)
                return new WorkerMarchState(worker, safePoint, 1f, this);

            if (hunkering is false)
                worker.StartCoroutine(EnsureSafety());

            if (safe is true)
                return new WorkerIdleState(worker);
            else
                return this;
        }
        public override void CloseState()
        {
            worker.StopCoroutine(EnsureSafety());
        }

        /// <summary>
        /// Wait to see if more damage is taken.
        /// </summary>
        /// <returns></returns>
        IEnumerator EnsureSafety()
        {
            hunkering = true;
            var duration = waitTime;
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                yield return null;
            }

            safe = true;
        }
    }
}
