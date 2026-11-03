using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(SphereCollider))]
public class OreNode : ResourceContainer
{
    public List<GameObject> oresModels;

    private void Awake()
    {
        CountChanged += ChangeOreVisual;
        resourceType = ResourceType.IRON;

    }

    private void ChangeOreVisual(int oreLeft) 
    {
        for (int i = oresModels.Count; i > oreLeft; i--)
            oresModels[i-1].gameObject.SetActive(false);
    }
}
