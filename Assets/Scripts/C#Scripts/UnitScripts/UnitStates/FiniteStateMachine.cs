using UnityEngine;

public class FiniteStateMachine : MonoBehaviour, IStateMachine
{

    State currentState;


    void Update() 
    {
        currentState = currentState.Act();
    }


    public void PushState(State state)
    {
        throw new System.NotImplementedException();
    }
}
