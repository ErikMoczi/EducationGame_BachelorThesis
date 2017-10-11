using Bachelor.MyExtensions.Managers;
using UnityEngine;

namespace Bachelor.UI
{
    public class SimpleSplashScreen : MonoBehaviour, ITransitionScene
    {
        private bool m_RequestNewScene = false;

        private void Update()
        {
            if (Input.anyKeyDown && !m_RequestNewScene)
            {
                m_RequestNewScene = true;
                this.LoadNewScene(2);
            }
        }
    }
}