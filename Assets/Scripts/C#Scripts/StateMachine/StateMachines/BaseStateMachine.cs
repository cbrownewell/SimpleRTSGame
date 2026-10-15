using StateMachine.States.Abstracts;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine.Abstracts
{
    public abstract class BaseStateMachine : IStateMachine
    {
        protected IState m_current;

        protected List<string> m_history;
        
        public BaseStateMachine(IState initial) 
        {
            m_current = initial;
            m_current.InitState();
        }

        public IState PeekState()
        {
            return m_current;
        }

        public void PushPriorityState(IState state)
        {
            m_current.CloseState();
            m_current = state;
            m_current.InitState();
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

            LogStateTransition(m_current, next);
            m_current.CloseState();
            m_current = next;
            m_current.InitState();
        }

        protected void LogStateTransition(IState current, IState next) 
        {
            string x = current.GetType().ToString();
            string y = next.GetType().ToString();
            Debug.Log("Transitioning from " + x + " to " + y + ".");
        }

        protected void LogStateContinuation(IState current)
        {
            string x = current.GetType().ToString();
            Debug.Log("Continuing " + x + ".");
        }
    }
}


