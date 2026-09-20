using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;
using Commands.Components;

public class WorkerUnitTests
{
    /// <summary>
    /// Makes sure the WorkerUnit class has all required components attached
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator CheckIfHasRequiredComponents_Success()
    {
        var gameObject = new GameObject();
        gameObject.AddComponent<WorkerUnit>();

        Assert.True(gameObject.TryGetComponent<ResourceInventory>(out _), "Worker should have a resource inventory.");
        
        //From GameUnit base class
        Assert.True(gameObject.TryGetComponent<CommandQueue>(out _), "Worker should have a command queue.");
        Assert.True(gameObject.TryGetComponent<CapsuleCollider>(out _), "Worker should have a capsule collider.");
        Assert.True(gameObject.TryGetComponent<NavMeshAgent>(out _), "Worker should have a navmesh agent.");
        Assert.True(gameObject.TryGetComponent<Rigidbody>(out _), "Worker should have a rigibody component.");

        yield return null;
    }

}
