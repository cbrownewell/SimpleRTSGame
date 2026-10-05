using StateMachine.States;
using StateMachine.States.Abstracts;
using System;
using System.Collections;
using UnityEngine;

namespace UnitScripts.States.Worker
{
    public class HarvestState : BaseWorkerState
    {
        public static event Func<ResourceType, Vector3, IHarvestable> FindResource;

        IHarvestable harvestTarget;

        bool harvesting = false;

        float harvestTime = 3f;//This should come from a matrix or dictionary

        public HarvestState(WorkerUnit owner) : base(owner)
        {
        }

        /// <summary>
        /// Use this overload for passing in a direct harvest target.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="harvestable"></param>
        public HarvestState(WorkerUnit owner, IHarvestable harvestable) : base(owner)
        {
            harvestTarget = harvestable;
        }

        public override void InitState()
        {
            //Look for an appropriate resource container
            if (harvestTarget is null || harvestTarget.IsEmpty())
                harvestTarget = FindResource.Invoke(harvestTarget.GetResourceType(), worker.transform.position);//Maybe use the position of the last target?
        }

        public override IState ExecuteState()
        {
            //If we don't have a harvestTarget then go to Idle State. aka we couldn't find one
            if (harvestTarget == null)
                return new WorkerIdleState(worker);

            //If we're in range of the harvestTarget then start harvesting, else keep moving towards it
            var track = Vector3.Distance(worker.transform.position, harvestTarget.Location.position);
            if (track >= worker.reach)
                return new WorkerMarchState(worker, harvestTarget.Location, worker.reach, this);
            else
                if (harvesting is false || worker.resourceInventory.InventoryFull() is false)
                {
                    worker.StartCoroutine(Harvest());
                    return this;
                }

            //Once we harvest we want to deposit the resource
            return new DepositState(worker, this);
        }
        public override void CloseState()
        {
            worker.StopCoroutine(Harvest());
        }
        IEnumerator Harvest()
        {
            harvesting = true;
            float duration = harvestTime;
            while (duration > 0)
                duration -= Time.deltaTime;

            var resource = harvestTarget.Extract();
            worker.resourceInventory.AddToInventory(resource);
            yield return null;
        }
    }
}


