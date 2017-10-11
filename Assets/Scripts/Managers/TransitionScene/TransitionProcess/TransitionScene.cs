using UnityEngine;
using System.Collections;
using Handler = System.Action<System.Object, System.Object>;
using UnityEngine.SceneManagement;
using Bachelor.Managers.Transition.Base;
using Bachelor.Utilities.Unity;

namespace Bachelor.Managers.Transition
{
    public class TransitionScene : TransitionProcess
    {
        [SerializeField]
        [ReadOnlyWhenPlaying]
        private string m_NewScene = "";

        private int NewSceneInt
        {
            get
            {
                int x = -1;
                int.TryParse(m_NewScene, out x);
                return x;
            }

            set
            {
                m_NewScene = value.ToString();
            }
        }

        private string NewSceneString
        {
            get
            {
                return m_NewScene;
            }

            set
            {
                m_NewScene = value;
            }
        }

        public void QuitApplication()
        {
            Application.Quit();
        }

        public void RestartApplication()
        {
            LoadNewScene(0);
        }

        public void RestartCurrentScene()
        {
            LoadNewScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadNewScene(int sceneIndex, bool animate, Handler handler = null)
        {
            if (sceneIndex < 0)
            {
                Debug.LogWarning("[TransitionScene] Wrong index of scene " + sceneIndex);
                return;
            }

            if (animate)
            {
                NewSceneInt = sceneIndex;
                PlayTransitionProcess(StartLoadSceneInt, handler);
            }
            else
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }

        public void LoadNewScene(int sceneIndex, Handler handler = null)
        {
            LoadNewScene(sceneIndex, true, handler);
        }

        public void LoadNewScene(int sceneIndex)
        {
            LoadNewScene(sceneIndex, null);
        }

        public void LoadNewScene(string sceneName, bool animate, Handler handler = null)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning("[TransitionScene] Can't load unnamed scene.");
                return;
            }

            if (animate)
            {
                NewSceneString = sceneName;
                PlayTransitionProcess(StartLoadSceneString, handler);
            }
            else
            {
                SceneManager.LoadScene(sceneName);
            }
        }

        public void LoadNewScene(string sceneName, Handler handler = null)
        {
            LoadNewScene(sceneName, true, handler);
        }

        public void LoadNewScene(string sceneName)
        {
            LoadNewScene(sceneName, null);
        }

        private IEnumerator LoadSceneAsync(int sceneIndex)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);
            yield return async;
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
            yield return async;
        }

        private void StartLoadSceneInt(object sender, object target)
        {
            StartCoroutine(LoadSceneAsync(NewSceneInt));
        }

        private void StartLoadSceneString(object sender, object target)
        {
            StartCoroutine(LoadSceneAsync(NewSceneString));
        }
    }
}