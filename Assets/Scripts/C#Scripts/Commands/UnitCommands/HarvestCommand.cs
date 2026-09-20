using System;
using System.Collections;
using UnityEngine;

namespace Commands.Unit
{
    public class HarvestCommand : Command
    {
        float _harvestTime;
        IHarvestable _harvestable;
        IResourceInventory _resourceInventory;
        MonoBehaviour _unit;

        /// <summary>
        /// Event invoked when the command needs a new target
        /// </summary>
        public event Func<Vector3, ResourceType, float, IHarvestable> GetNewHarvestable;
        public override bool IsFinished => _resourceInventory.InventoryFull();

        public HarvestCommand(IHarvestable harvestObj, IResourceInventory inventory, MonoBehaviour unit)
        {
            _harvestable = harvestObj;
            _resourceInventory = inventory;
            _harvestTime = harvestObj.GetBaseHarvestTime();
            _unit = unit;
        }

        public override void Execute()
        {
            if (_harvestable.IsEmpty() is false)
                _unit.StartCoroutine(Harvest());
            else
                _harvestable = FindNewResource();
            
        }

        /// <summary>
        /// Calls locator service to find the closest resource node that can be harvested from.
        /// </summary>
        /// <returns></returns>
        IHarvestable FindNewResource() 
        {
            var startPosition = _unit.gameObject.transform.position;
            return GetNewHarvestable?.Invoke(startPosition, _harvestable.GetResourceType(), 10.0f);
        }

        IEnumerator Harvest()
        {
            ///This will need better logic to make canceling or mid harvest changing more fluent
            if (_harvestable.IsEmpty() || _harvestable.GetResourceType() is ResourceType.NULL) 
            {
                Cancel();
                yield return null;
            }

            yield return new WaitForSeconds(_harvestTime);
            var resource = _harvestable.Extract();
            _resourceInventory.AddToInventory(resource);
        }

        public override void Cancel()
        {
            _unit.StopCoroutine(Harvest());
            base.Cancel();
        }
    }
}
