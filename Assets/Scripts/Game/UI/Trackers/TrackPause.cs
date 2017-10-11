using Bachelor.Game.Base;
using Bachelor.Game.Controllers;
using Bachelor.MyExtensions.Managers;
using UnityEngine;

namespace Bachelor.Game
{
    public class TrackPause : TrackBase, INotification
    {
        [SerializeField]
        private GameObject m_PauseMenu;

        [SerializeField]
        private GameController m_GameController;

        private void OnEnable()
        {
            this.AddObserver(OnDisplayPauseBar, GameControllerBase.PauseNotification);
            this.AddObserver(OnHidePauseBar, GameControllerBase.ResumeNotification);
        }

        private void OnDisable()
        {
            this.RemoveObserver(OnDisplayPauseBar, GameControllerBase.PauseNotification);
            this.RemoveObserver(OnHidePauseBar, GameControllerBase.ResumeNotification);
        }

        public void DisablePause()
        {
            m_PauseMenu.SetActive(false);
            m_GameController.Pause = false;
        }

        private void OnDisplayPauseBar(object arg1, object arg2)
        {
            m_PauseMenu.SetActive(true);
        }

        private void OnHidePauseBar(object arg1, object arg2)
        {
            m_PauseMenu.SetActive(false);
        }
    }
}