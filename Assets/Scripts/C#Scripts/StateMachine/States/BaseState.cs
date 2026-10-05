namespace StateMachine.States.Abstracts 
{
    public abstract class BaseState : IState
    {
        public abstract void InitState();
        public abstract IState ExecuteState();
        public abstract void CloseState();
    }
}


