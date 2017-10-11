using UnityEngine;

namespace Bachelor.Managers.Transition.Base
{
    public partial class TransitionProcess
    {
        private interface ITransitionDetailsExtended : ITransitionDetails
        {
            void SetInterpolation(AnimationCurve value);
            void SetTransitionDuration(float value);
            void SetTransitionStyle(TransitionStyle value);
            void SetTransitionLoop(bool value);
            void SetVisibleAfterTransition(bool value);
            void SetTransitionOverlay(CanvasGroup value);
            void SetTransitionElementChild(TransitionElement value);
        }
    }
}