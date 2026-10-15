using NUnit.Framework;
using System.Collections;
using UnitScripts.States.Worker;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class WorkerStateMachineTests
{
    WorkerUnit m_workerUnit;
    Transform m_moveTestPoint;
    Transform m_oreNode;


    /// <summary>
    /// Sets the test environment. Called before each test.
    /// </summary>
    [UnitySetUp]
    public IEnumerator SetupBaseTestEnvironment()
    {
        SceneManager.LoadScene("StateTesting", LoadSceneMode.Single);

        while (SceneManager.GetSceneByName("StateTesting").isLoaded is false)
        {
            Debug.Log("Scene not loaded");
            yield return null;
        }

        m_workerUnit = GameObject.Find("Worker").GetComponent<WorkerUnit>();
        m_moveTestPoint = GameObject.Find("MovePoint").transform;
        m_oreNode = GameObject.Find("OreNode").transform;
        
    }

    /// <summary>
    /// Ensures the Worker unit is initialized in the Idle state.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator WorkerIdleOnSpawn()
    {
        var current = m_workerUnit.stateMachine.PeekState().GetType();

        Assert.True(Equals(current, typeof(WorkerIdleState)), $"Worker does not spawn in idle state, instead was {current}.");

        yield return null;
    }

    /// <summary>
    /// The movement state should ensure that a worker reaches the location
    /// </summary>
    /// <param name="waitTime">How long the test is allowed to take before considering it to be a failure.</param>
    /// <returns></returns>
    [UnityTest]
    [Timeout(6000)]
    public IEnumerator MovementStatePaths_NoStateCallback() 
    {
        var marchState = new WorkerMarchState(m_workerUnit, m_moveTestPoint, m_workerUnit.Reach);

        m_workerUnit.stateMachine.PushPriorityState(marchState);

        //Ensure we are in a march state
        var current = m_workerUnit.stateMachine.PeekState();
        Assert.True(Equals(current.GetType(), typeof(WorkerMarchState)), $"Worker did not transition to the marchState, instead was {current}.");

        float timeCount = 0;
        //1st cond is to stop a infinite loop, 2nd is to check when the movement is done.
        while (marchState.PercentLeft > 0)
        {
            timeCount += Time.deltaTime;
            yield return null;
        }

        //Ensure we return to the idle state by default
        Assert.True(Equals(current, typeof(WorkerIdleState)), $"Worker did not return to an idle state, instead was {current}.");
    }

    /// <summary>
    /// Ensure the movement state can properly callback to its requestor state.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator MovementStatePaths_StateCallback()
    {

        yield return null;
    }

    /// <summary>
    /// Ensure the harvest state actually retreives a resource.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator HavestStateRetreivesResources()
    {
        var harvestState = new HarvestState(m_workerUnit, m_oreNode.GetComponent<IHarvestable>());

        m_workerUnit.stateMachine.PushPriorityState(harvestState);

        //Ensure we are in a harvest state
        var current = m_workerUnit.stateMachine.PeekState().GetType();
        Assert.True(Equals(current, typeof(HarvestState)), $"HarvestState was not pushed correctly, instead was {current}.");
        var dist = Vector3.Distance(m_workerUnit.transform.position, m_oreNode.transform.position);
        yield return new WaitForSeconds(10f);

        //Worker should have harvested a resource
        Assert.True(m_workerUnit.resourceInventory.InventoryFull());
    }

    /// <summary>
    /// Test to make sure the Flee state can retrieve a location.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator FleeState_RetrieveSafePosition()
    {

        yield return null;
    }


}
