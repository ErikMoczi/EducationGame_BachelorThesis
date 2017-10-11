using Bachelor.Managers;
using Handler = System.Action<System.Object, System.Object>;

namespace Bachelor.MyExtensions.Managers
{
    public static class ITransitionSceneExtensions
    {
        public static void QuitApplication(this ITransitionScene obj)
        {
            TransitionSceneManager.Instance.QuitApplication();
        }

        public static void RestartApplication(this ITransitionScene obj)
        {
            TransitionSceneManager.Instance.RestartApplication();
        }

        public static void RestartCurrentScene(this ITransitionScene obj)
        {
            TransitionSceneManager.Instance.RestartCurrentScene();
        }

        public static void LoadNewScene(this ITransitionScene obj, int sceneIndex)
        {
            TransitionSceneManager.Instance.LoadNewScene(sceneIndex);
        }

        public static void LoadNewScene(this ITransitionScene obj, int sceneIndex, Handler handler)
        {
            TransitionSceneManager.Instance.LoadNewScene(sceneIndex, handler);
        }

        public static void LoadNewScene(this ITransitionScene obj, int sceneIndex, bool animate, Handler handler = null)
        {
            TransitionSceneManager.Instance.LoadNewScene(sceneIndex, animate, handler);
        }

        public static void LoadNewScene(this ITransitionScene obj, string sceneName)
        {
            TransitionSceneManager.Instance.LoadNewScene(sceneName);
        }

        public static void LoadNewScene(this ITransitionScene obj, string sceneName, Handler handler)
        {
            TransitionSceneManager.Instance.LoadNewScene(sceneName, handler);
        }

        public static void LoadNewScene(this ITransitionScene obj, string sceneName, bool animate, Handler handler = null)
        {
            TransitionSceneManager.Instance.LoadNewScene(sceneName, animate, handler);
        }
    }
}