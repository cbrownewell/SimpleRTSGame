using StateMachine.Abstracts;
using StateMachine.States.Abstracts;

namespace UnitScripts.StateMachines 
{
    public class WorkerStateMachine : BaseStateMachine
    {
        private readonly WorkerUnit worker;
        public WorkerStateMachine(IState initial, WorkerUnit owner) : base(initial)
        {
            worker = owner;
        }
    }
}