using System.Collections.Generic;
using UnityEngine;

public class OreNode : ResourceNode
{
    public List<GameObject> ores;

    private void Awake()
    {
        //postHarvest += ChangeOreVisual;//This should be an event
    }

    private void Start()
    {
        container = new ResourceContainer(totalAmount, resourceType);
    }

    public void ChangeOreVisual(int oreLeft) 
    {
        for (int i = ores.Count; i > oreLeft; i--)
        {
            ores[i-1].gameObject.SetActive(false);
        }
    }
}
