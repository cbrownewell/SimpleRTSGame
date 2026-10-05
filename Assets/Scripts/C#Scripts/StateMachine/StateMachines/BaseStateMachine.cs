using StateMachine.States.Abstracts;

namespace StateMachine.Abstracts
{
    public abstract class BaseStateMachine : IStateMachine
    {
        protected IState current;
        
        public BaseStateMachine(IState initial) 
        {
            current = initial;
            current.InitState();
        }

        public IState PeekState()
        {
            return current;
        }

        public void PushPriorityState(IState state)
        {
            current.CloseState();
            current = state;
            current.InitState();
        }

        public void Tick()
        {
            //Execute the current state and see whats next
            var next = current.ExecuteState();

            //if next is the same as current then we don't need to change anything
            if (ReferenceEquals(current, next))
                return;

            current.CloseState();
            current = next;
            current.InitState();

        }
    }
}


