using Bachelor.Game.Base;
using Bachelor.MyExtensions.Managers;
using UnityEngine;

namespace Bachelor.Menu
{
    public class ApplicationManager : MonoBehaviour, ITransitionScene
    {
        public void LoadScene(int sceneIndex)
        {
            this.LoadNewScene(sceneIndex);
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		 this.QuitApplication();
#endif
        }

        public void LoadPlanetLevel(int planetId)
        {
            SharedObject.SetString("planetLevel", planetId.ToString());
            LoadScene(4);
        }
    }
}