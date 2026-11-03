using NUnit.Framework;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
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
            //Debug.Log("Scene not loaded");
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
    public IEnumerator MovementStatePaths_NoStateCallback() 
    {
        var marchState = new WorkerMarchState(m_workerUnit, m_moveTestPoint);
        m_workerUnit.stateMachine.PushPriorityState(marchState);

        //Ensure we are in a march state
        var current = m_workerUnit.stateMachine.PeekState();
        Assert.True(Equals(current.GetType(), typeof(WorkerMarchState)), $"Worker did not transition to the marchState, instead was {current}.");

        //1st cond is to stop a infinite loop, 2nd is to check when the movement is done.
        while (marchState.Arrived is false) 
            yield return null;
        
        //Ensure we return to the idle state by default
        yield return 0;
        current = m_workerUnit.stateMachine.PeekState();
        Assert.True(Equals(current.GetType(), typeof(WorkerIdleState)), "Worker did not return to an idle state, should have been " + typeof(WorkerIdleState) + $" instead was {current}.");
    }

    /// <summary>
    /// Ensure the movement state can properly callback to its requestor state.
    /// </summary>
    /// <returns></returns>
    [UnityTest]
    public IEnumerator MovementStatePaths_StateCallback()
    {
        var fleeState = new FleeState(m_workerUnit);
        
        var marchState = new WorkerMarchState(m_workerUnit, m_moveTestPoint, fleeState);
        m_workerUnit.stateMachine.PushPriorityState(marchState);

        //Ensure we are in a march state
        var current = m_workerUnit.stateMachine.PeekState();
        Assert.True(Equals(current.GetType(), typeof(WorkerMarchState)), $"Worker did not transition to " + typeof(WorkerMarchState) + " state correctly, instead was {current}.");

        //1st cond is to stop a infinite loop, 2nd is to check when the movement is done.
        while (marchState.Arrived is false)
            yield return null;

        //Ensure we return to the idle state by default
        yield return 0;//Waits one frame
        current = m_workerUnit.stateMachine.PeekState();
        Assert.True(Equals(current.GetType(), typeof(FleeState)), "Worker did not transition state correctly, should have been " + typeof(FleeState) + $" instead was {current}.");
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
        var current = m_workerUnit.stateMachine.PeekState();
        Assert.True(Equals(current.GetType(), typeof(HarvestState)), $"HarvestState was not pushed correctly, instead was {current}.");
        var dist = Vector3.Distance(m_workerUnit.transform.position, m_oreNode.transform.position);
        yield return new WaitForSeconds(8f);

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
        var fleeState = new FleeState(m_workerUnit);
        m_workerUnit.stateMachine.PushPriorityState(fleeState);
        var current = m_workerUnit.stateMachine.PeekState();
        Assert.True(Equals(current.GetType(), typeof(FleeState)), $"Worker did not transition to the flee state, instead was {current}.");
        yield return 0;

        current = m_workerUnit.stateMachine.PeekState();
        Assert.True(Equals(current.GetType(), typeof(WorkerMarchState)), "Worker did not transition correctly, should have been " + typeof(WorkerMarchState) + $" instead was {current}.");

    }


}
