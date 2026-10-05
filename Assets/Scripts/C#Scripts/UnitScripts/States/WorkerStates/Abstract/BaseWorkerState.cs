using StateMachine.States.Abstracts;

public abstract class BaseWorkerState : BaseState
{
    protected WorkerUnit worker;
    public BaseWorkerState(WorkerUnit owner) 
    {
         worker = owner;
    }
}
