using Bachelor.Managers.Transition;
using System.Collections;

namespace Bachelor.Managers
{
    public partial class TransitionSceneManager
    {
        private interface ITransitionStatusControl : ITransitionStatus
        {
            new bool InTransition { get; set; }
            new float TransitionPercent { get; set; }
            new bool TransitionFading { get; set; }
            float TransitionPercentBreak { get; set; }
            IEnumerator Coroutine { get; set; }
        }
    }
}