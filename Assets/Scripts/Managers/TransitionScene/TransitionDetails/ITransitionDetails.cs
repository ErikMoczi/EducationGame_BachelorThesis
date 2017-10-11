using UnityEngine;

namespace Bachelor.Managers.Transition.Base
{
    public interface ITransitionDetailsBase
    {
        AnimationCurve Interpolation { get; }
        float TransitionDuration { get; }
        CanvasGroup TransitionOverlay { get; }
        ITransitionElement TransitionElementChild { get; }
    }

    public interface ITransitionDetails : ITransitionDetailsBase
    {
        TransitionStyle TransitionStyle { get; }
        bool TransitionLoop { get; }
        bool TransitionOnWake { get; }
        bool VisibleAfterTransition { get; }
    }
}