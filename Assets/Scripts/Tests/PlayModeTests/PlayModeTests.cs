using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

public class PlayModeTests
{
    [UnityTest] //TODO: Finish this test somehow
    public IEnumerator PlayModeTest_CheckIfGameObjectHasClass_Success()
    {
        var mainObj = new GameObject();
        mainObj.AddComponent<WorkerUnitMono>();
        mainObj.AddComponent<NavMeshAgent>();

        //var hasClass = mainObj.CheckComponentsForType<ISelectable>();

        //Assert
        //Assert.IsTrue(hasClass);
        yield return null;
    }
}
