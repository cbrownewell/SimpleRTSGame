using Mono.Cecil;
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
                harvestTarget = FindResource?.Invoke(harvestTarget.GetResourceType(), worker.transform.position);//Maybe use the position of the last target?
        }

        public override IState ExecuteState()
        {
            //If we don't have a harvestTarget then go to Idle State. aka we couldn't find one
            if (harvestTarget == null)
                return new WorkerIdleState(worker);

            //If we're in range of the harvestTarget then start harvesting, else keep moving towards it
            var track = Vector3.Distance(worker.transform.position, harvestTarget.Location.position);//Issue is that the movement state and harvest state are targetting different point. Harvest targets actual object while movement creates a point on the navmesh
            if (track >= worker.Reach)
            {
                Debug.Log("Worker not in reach to harvest.  Too far at " + track);
                return new WorkerMarchState(worker, harvestTarget.Location, this);
            }
            else 
            {
                if (harvesting is false)
                {
                    if(worker.resourceInventory.InventoryFull() is false)
                        worker.StartCoroutine(Harvest());
                    else
                        return new DepositState(worker, this); //Once we harvest we want to deposit the resource
                }

                return this;
            }
        }
        public override void CloseState()
        {
            worker.StopCoroutine(Harvest());
        }
        IEnumerator Harvest()
        {
            harvesting = true;
            worker.Animator.Play("Mining");
            harvestTarget.Extract(OnHarvestEnd);
            yield return new WaitUntil(() => harvesting is false);
        }

        void OnHarvestEnd(ResourceType type)
        {
            harvesting = false;
            worker.Animator.Play("Idle");
            worker.resourceInventory.AddToInventory(type);
        }
    }
}


