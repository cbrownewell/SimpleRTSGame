using StateMachine.States.Abstracts;
using System;
using System.Collections;
using UnityEngine;

namespace UnitScripts.States.Worker 
{
    public class FleeState : BaseWorkerState
    {
        public static event Func<Vector3, float, Transform> FindSafePoint;

        Transform safePoint;
        public Vector3 TargetPoint { get { return safePoint.position; } }

        float waitTime;

        bool hunkering = false;
        bool safe = false;

        public FleeState(WorkerUnit owner) : base(owner)
        {
        }

        public override void InitState()
        {
            //Find a safe distance away
            safePoint = FindSafePoint?.Invoke(worker.transform.position, worker.Awareness);
        }

        public override IState ExecuteState()
        {
            if (Vector3.Distance(worker.transform.position, safePoint.position) >= worker.Reach)
                return new WorkerMarchState(worker, safePoint, this);

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
