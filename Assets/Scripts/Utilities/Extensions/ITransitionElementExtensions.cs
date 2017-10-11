using Bachelor.Managers;
using Bachelor.Managers.Transition;
using Handler = System.Action<System.Object, System.Object>;

namespace Bachelor.MyExtensions.Managers
{
    public static class TransitionExtensions
    {
        public static void PlayTransition(this ITransitionElement obj)
        {
            TransitionSceneManager.Instance.PlayTransition(obj);
        }

        public static void PlayTransition(this ITransitionElement obj, Handler handler)
        {
            TransitionSceneManager.Instance.PlayTransition(obj, handler);
        }

        public static void StopTransition(this ITransitionElement obj)
        {
            TransitionSceneManager.Instance.StopTransition(obj);
        }

        public static void StopTransition(this ITransitionElement obj, bool visible)
        {
            TransitionSceneManager.Instance.StopTransition(obj, visible);
        }

        public static ITransitionStatus GetTransitionStatus(this ITransitionElement obj)
        {
            return TransitionSceneManager.Instance.GetTransitionStatus(obj);
        }

        public static bool InTransitionSafe(this ITransitionElement obj)
        {
            ITransitionStatus transitionStatus = TransitionSceneManager.Instance.GetTransitionStatus(obj);
            return transitionStatus != null ? transitionStatus.InTransition : false;
        }
    }
}