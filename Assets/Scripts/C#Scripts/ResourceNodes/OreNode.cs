using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class OreNode : ResourceContainer
{
    public List<GameObject> ores;

    private void Awake()
    {
        //postHarvest += ChangeOreVisual;//This should be an event
    }

    public void ChangeOreVisual(int oreLeft) 
    {
        for (int i = ores.Count; i > oreLeft; i--)
        {
            ores[i-1].gameObject.SetActive(false);
        }
    }
}
