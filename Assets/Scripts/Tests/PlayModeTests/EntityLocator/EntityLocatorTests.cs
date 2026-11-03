using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class EntityLocatorTests
{
    EntityLocator m_locator;

    /// <summary>
    /// Sets the test environment. Called before each test.
    /// </summary>
    [UnitySetUp]
    public IEnumerator SetupBaseTestEnvironment()
    {
        SceneManager.LoadScene("StateTesting", LoadSceneMode.Single);
        while (SceneManager.GetSceneByName("StateTesting").isLoaded is false)
            yield return null;

        m_locator = Camera.main.GetComponent<EntityLocator>();
    }

    /// <summary>
    /// Ensure the Entity Locator can locate a depot
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator LocateDepot() 
    {
        var entrance = m_locator.LocateDepotEntrance(new Vector3());

        Assert.IsNotNull(entrance, "Unable to locate a depot.");
        yield return null;
    }


    /// <summary>
    /// Ensure the Entity Locator can locate a resource
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator LocateResource()
    {
        var harvestable = m_locator.LocateHarvestable(ResourceType.IRON, Vector3.zero);

        Assert.IsNotNull(harvestable, "Unable to locate a resource.");
        yield return null;
    }

}
