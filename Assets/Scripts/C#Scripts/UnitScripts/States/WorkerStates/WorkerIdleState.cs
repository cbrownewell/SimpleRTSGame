using StateMachine.States.Abstracts;

namespace UnitScripts.States.Worker
{
    public class WorkerIdleState : BaseWorkerState
    {
        public WorkerIdleState(WorkerUnit owner) : base(owner)
        {
        }

        public override void CloseState()
        {
            
        }

        public override IState ExecuteState()
        {
            return this;
        }

        public override void InitState()
        {

        }
    }
}


