using System.Collections;

namespace Bachelor.Managers
{
    public partial class TransitionSceneManager
    {
        private class TransitionStatusControl : ITransitionStatusControl
        {
            private const float m_TransitionPercentMin = 0f;

            public static float TransitionPercentMin
            {
                get
                {
                    return m_TransitionPercentMin;
                }
            }

            private const float m_TransitionPercentMax = 1f;

            public static float TransitionPercentMax
            {
                get
                {
                    return m_TransitionPercentMax;
                }
            }

            private bool m_InTransition = false;

            public bool InTransition
            {
                get
                {
                    return m_InTransition;
                }

                set
                {
                    m_InTransition = value;
                }
            }

            private float m_TransitionPercent = 0f;

            public float TransitionPercent
            {
                get
                {
                    return m_TransitionPercent;
                }

                set
                {
                    m_TransitionPercent = CalculateTransitionPercent(value);
                }
            }

            private float m_TransitionPercentBreak = 0f;

            public float TransitionPercentBreak
            {
                get
                {
                    return m_TransitionPercentBreak;
                }

                set
                {
                    m_TransitionPercentBreak = value;
                }
            }

            private IEnumerator m_Coroutine = null;

            public IEnumerator Coroutine
            {
                get
                {
                    return m_Coroutine;
                }

                set
                {
                    m_Coroutine = value;
                }
            }

            private bool m_TransitionFading = false;

            public bool TransitionFading
            {
                get
                {
                    return m_TransitionFading;
                }

                set
                {
                    m_TransitionFading = value;
                }
            }

            private float CalculateTransitionPercent(float newValue)
            {
                if (newValue > m_TransitionPercentMax)
                {
                    return m_TransitionPercentMax;
                }
                else if (newValue < m_TransitionPercentMin)
                {
                    return m_TransitionPercentMin;
                }
                else
                {
                    return newValue;
                }
            }
        }
    }
}