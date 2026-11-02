using StateMachine.States.Abstracts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateMachine.Abstracts
{
    public abstract class BaseStateMachine : IStateMachine
    {
        protected IState m_current;

        protected List<string> m_history;

        string lastLog;
        
        public BaseStateMachine(IState initial) 
        {
            Debug.Log("Starting unit with " + initial.GetType().ToString().Split('.').Last());
            m_current = initial;
            m_current.InitState();
        }

        public IState PeekState()
        {
            return m_current;
        }

        public void PushPriorityState(IState state)
        {
            TransitionState(state);
        }

        public void Tick()
        {
            //Execute the current state and see whats next
            var next = m_current.ExecuteState();

            //if next is the same as current then we don't need to change anything
            if (ReferenceEquals(m_current, next)) 
            {
                LogStateContinuation(next);
                return;
            }

            TransitionState(next);
        }

        private void TransitionState(IState next) 
        {
            LogStateTransition(m_current, next);
            m_current.CloseState();
            m_current = next;
            m_current.InitState();
        }


        protected void LogStateTransition(IState current, IState next) 
        {
            string x = current.GetType().ToString().Split('.').Last();
            string y = next.GetType().ToString().Split('.').Last();
            var newLog = "Transitioning from " + x + " to " + y + ".";

            if (newLog != lastLog) 
            {
                Debug.Log(newLog);
                lastLog = newLog;
            }
        }

        protected void LogStateContinuation(IState current)
        {
            string x = current.GetType().ToString().Split('.').Last();
            var newLog = "Continuing " + x + ".";

            if (newLog != lastLog)
            {
                Debug.Log(newLog);
                lastLog = newLog;
            }
        }
    }
}


