namespace StateMachine.States.Abstracts
{
    public interface IState
    {
        /// <summary>
        /// Executes steps needed to initialize a state.
        /// </summary>
        /// <remarks>Use this to grab initial data, etc. Think Start or Awake on Monobehavior.</remarks>
        void InitState();
        /// <summary>
        /// Executes the core functionality of the state.
        /// </summary>
        /// <returns></returns>
        IState ExecuteState();
        /// <summary>
        /// Executes any steps needed to close out the state.
        /// </summary>
        /// <remarks>Use this to stop coroutines, refund resources, etc.</remarks>
        void CloseState();
    }

}

