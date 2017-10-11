using UnityEngine;

namespace Bachelor.Managers.Transition.Base
{
    public partial class TransitionProcess
    {
        private class TransitionDetailsExtended : TransitionDetails, ITransitionDetailsExtended
        {
            public void SetInterpolation(AnimationCurve value)
            {
                m_Interpolation = value;
            }

            public void SetTransitionDuration(float value)
            {
                m_TransitionDuration = value;
            }

            public void SetTransitionElementChild(TransitionElement value)
            {
                m_TransitionElementChild = value;
            }

            public void SetTransitionLoop(bool value)
            {
                m_TransitionLoop = value;
            }

            public void SetTransitionOverlay(CanvasGroup value)
            {
                m_TransitionOverlay = value;
            }

            public void SetTransitionStyle(TransitionStyle value)
            {
                m_TransitionStyle = value;
            }

            public void SetVisibleAfterTransition(bool value)
            {
                m_VisibleAfterTransition = value;
            }

            public TransitionDetailsExtended(
                ITransitionDetailsBase transitionDetailsBase,
                bool transitionOnWake,
                TransitionStyle transitionStyle,
                bool transitionLoop,
                bool visibleAfterTransition
            ) : base(
                transitionDetailsBase,
                transitionOnWake,
                transitionStyle,
                transitionLoop,
                visibleAfterTransition
            )
            {
            }
        }
    }
}