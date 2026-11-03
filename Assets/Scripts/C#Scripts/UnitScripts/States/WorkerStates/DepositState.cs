using StateMachine.States.Abstracts;
using System;
using UnityEngine;

namespace UnitScripts.States.Worker
{
    public class DepositState : BaseWorkerState
    {
        public static event Func<Vector3, Transform> FindDepot;

        Transform dropoffDepot;
        public Transform DropOffDepot { get { return dropoffDepot; } }

        HarvestState origin;

        public DepositState(WorkerUnit owner, HarvestState state) : base(owner) //this needs the ability to take in a depot to a unit can be order to depot if interupted
        {
            origin = state;
        }
        public override void InitState()
        {
            //Locate deposit point
            dropoffDepot = FindDepot?.Invoke(worker.transform.position);

            Debug.Log("Depot is located at " +  dropoffDepot.position);

            if (dropoffDepot is null)
                Debug.LogError("Depot not found.");
        }

        public override IState ExecuteState()
        {
            //If we can't find a depot then we idle.
            if (dropoffDepot is null)
                return new WorkerIdleState(worker);

            //Move to Deposit point
            var diff = Vector3.Distance(worker.transform.position, dropoffDepot.position);
            if (diff >= worker.Reach)
                return new WorkerMarchState(worker, dropoffDepot, this);

            //Deposit - this will need to eventually put the resource in a global repository, but we just discard for now
            var resource = worker.resourceInventory.GetResourceFromInventory();

            //Resume
            return origin;
        }

        public override void CloseState()
        {

        }
    }
}
