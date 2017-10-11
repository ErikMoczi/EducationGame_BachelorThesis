using Bachelor.Utilities;

namespace Bachelor.Game.Base
{
    public abstract class StateMachine : SingletonMonoBehaviour<StateMachine>
    {
        private State m_CurrentState;

        public State CurrentState
        {
            get
            {
                return m_CurrentState;
            }
            set
            {
                Transition(value);
            }
        }

        private bool m_InTransition;

        public bool InTransition
        {
            get
            {
                return m_InTransition;
            }
        }

        protected StateMachine() { }

        public T GetState<T>() where T : State
        {
            T target = GetComponent<T>();
            if (target == null)
            {
                target = gameObject.AddComponent<T>();
            }
            
            return target;
        }

        public void ChangeState<T>() where T : State
        {
            CurrentState = GetState<T>();
        }

        private void Transition(State newState)
        {
            if (m_CurrentState == newState || m_InTransition)
                return;

            m_InTransition = true;

            if (m_CurrentState != null)
            {
                m_CurrentState.End();
            }                

            m_CurrentState = newState;

            if (m_CurrentState != null)
            {
                m_CurrentState.Begin();
            }                

            m_InTransition = false;
        }
    }
}