using StateMachine.States.Abstracts;

namespace StateMachine.Abstracts 
{
    public interface IStateMachine
    {
        /// <summary>
        /// Overrides the current state for the given state. Current state is closed and the given state is initializeed.
        /// </summary>
        /// <param name="state"></param>
        void PushPriorityState(IState state);
        /// <summary>
        /// Shows the current state the machine is processing.
        /// </summary>
        /// <returns></returns>
        IState PeekState();
        /// <summary>
        /// Resolves the current state.
        /// </summary>
        void Tick();
    }
}


