using UnityEngine;
using System;

namespace Bachelor.Managers.Transition.Base
{
    public enum TransitionStyle
    {
        In,
        Out,
        InOut,
        None
    }

    [Serializable]
    public class TransitionDetails : TransitionDetailsBase, ITransitionDetails
    {
        [SerializeField]
        protected bool m_TransitionOnWake = false;

        public bool TransitionOnWake
        {
            get
            {
                return m_TransitionOnWake;
            }
        }

        [SerializeField]
        protected TransitionStyle m_TransitionStyle = TransitionStyle.None;

        public TransitionStyle TransitionStyle
        {
            get
            {
                return m_TransitionStyle;
            }
        }

        [SerializeField]
        protected bool m_TransitionLoop = false;

        public bool TransitionLoop
        {
            get
            {
                return m_TransitionLoop;
            }
        }

        [SerializeField]
        protected bool m_VisibleAfterTransition = false;

        public bool VisibleAfterTransition
        {
            get
            {
                return m_VisibleAfterTransition;
            }
        }

        public TransitionDetails(
            AnimationCurve interpolation,
            float transitionDuration,
            CanvasGroup transitionOverlay,
            ITransitionElement transitionElementChild,
            bool transitionOnWake,
            TransitionStyle transitionStyle,
            bool transitionLoop,
            bool visibleAfterTransition
        ) : base(interpolation, transitionDuration, transitionOverlay, transitionElementChild)
        {
            m_TransitionOnWake = transitionOnWake;
            m_TransitionStyle = transitionStyle;
            m_TransitionLoop = transitionLoop;
            m_VisibleAfterTransition = visibleAfterTransition;
        }

        public TransitionDetails(
            ITransitionDetails transitionDetails
        ) : base(transitionDetails)
        {
            m_TransitionOnWake = transitionDetails.TransitionOnWake;
            m_TransitionStyle = transitionDetails.TransitionStyle;
            m_TransitionLoop = transitionDetails.TransitionLoop;
            m_VisibleAfterTransition = transitionDetails.VisibleAfterTransition;
        }

        public TransitionDetails(
            ITransitionDetailsBase transitionDetailsBase,
            bool transitionOnWake,
            TransitionStyle transitionStyle,
            bool transitionLoop,
            bool visibleAfterTransition
        ) : base(transitionDetailsBase)
        {
            m_TransitionOnWake = transitionOnWake;
            m_TransitionStyle = transitionStyle;
            m_TransitionLoop = transitionLoop;
            m_VisibleAfterTransition = visibleAfterTransition;
        }
    }
}