using UnityEngine;
using System;

namespace Bachelor.Managers.Transition.Base
{
    [Serializable]
    public class TransitionDetailsBase : ITransitionDetailsBase
    {
        private const float m_TransitionDurationMin = 0f;

        public static float TransitionDurationMin
        {
            get
            {
                return m_TransitionDurationMin;
            }
        }

        private const float m_TransitionDurationMax = 5f;

        public static float TransitionDurationMax
        {
            get
            {
                return m_TransitionDurationMax;
            }
        }

        [SerializeField]
        protected AnimationCurve m_Interpolation;

        public AnimationCurve Interpolation
        {
            get
            {
                return m_Interpolation;
            }
        }

        [SerializeField, Range(m_TransitionDurationMin, m_TransitionDurationMax)]
        protected float m_TransitionDuration = 2f;

        public float TransitionDuration
        {
            get
            {
                return m_TransitionDuration;
            }
        }

        [SerializeField]
        protected CanvasGroup m_TransitionOverlay;

        public CanvasGroup TransitionOverlay
        {
            get
            {
                return m_TransitionOverlay;
            }
        }

        [SerializeField]
        protected TransitionElement m_TransitionElementChild = null;

        public ITransitionElement TransitionElementChild
        {
            get
            {
                return m_TransitionElementChild;
            }
        }

        public TransitionDetailsBase(
            AnimationCurve interpolation,
            float transitionDuration,
            CanvasGroup transitionOverlay,
            ITransitionElement transitionElementChild
        )
        {
            m_Interpolation = interpolation;
            m_TransitionDuration = transitionDuration;
            m_TransitionOverlay = transitionOverlay;
            m_TransitionElementChild = transitionElementChild as TransitionElement;
        }

        public TransitionDetailsBase(ITransitionDetailsBase transitionDetailsBase) : this(
            transitionDetailsBase.Interpolation,
            transitionDetailsBase.TransitionDuration,
            transitionDetailsBase.TransitionOverlay,
            transitionDetailsBase.TransitionElementChild)
        {
        }
    }
}