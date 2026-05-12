using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class ResourceNodeTests
{

    /// <summary>
    /// Checks if the ResourceNode extracts a resource
    /// </summary>
    [UnityTest]
    public IEnumerator ExtractProperAmount_Success()
    {
        ResourceContainer container = new GameObject().AddComponent<OreNode>();
        container.maxAmount = 5;
        container.InitContainer();
        
        IHarvestable harvestable = container;
        harvestable.Extract();

        Assert.True(container.resourcesLeft == container.maxAmount - 1);
        yield return null;
    }

    /// <summary>
    /// Checks if the ResourceNode detects if it is empty
    /// </summary>
    [UnityTest]
    public IEnumerator CheckIfEmpty_Success()
    {
        IHarvestable harvestable = new GameObject().AddComponent<OreNode>();

        yield return null;
    }

    /// <summary>
    /// Checks if the ResourceNode detects if it is not empty
    /// </summary>
    [UnityTest]
    public IEnumerator CheckIfNotEmpty_Success()
    {
        IHarvestable harvestable = new GameObject().AddComponent<OreNode>();

        yield return null;
    }
}
