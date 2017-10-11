using Bachelor.Game.Base;
using Bachelor.Game.Controllers.GM.Base;
using Bachelor.MyExtensions.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Bachelor.Game
{
    public class TrackAnnouncer : TrackBase, INotification
    {
        [SerializeField]
        private Text m_HeaderText;

        [SerializeField]
        private Text m_ContentText;

        [SerializeField]
        private GameObject m_AnnouncePanel;

        private void OnEnable()
        {
            this.AddObserver(OnGameComplete, PlayingState.GameCompleteNotification);
            this.AddObserver(OnGameOver, PlayingState.GameOverNotification);
        }

        private void OnDisable()
        {
            this.RemoveObserver(OnGameComplete, PlayingState.GameCompleteNotification);
            this.RemoveObserver(OnGameOver, PlayingState.GameOverNotification);
        }        

        private void Start()
        {
            HideAnnouncePanel();
        }

        private void OnGameComplete(object sender, object target)
        {
            ShowAnnouncePanel();
            m_HeaderText.text = "Game Complete";
            m_ContentText.text = "Congratulation you have past challenge";
        }

        private void OnGameOver(object sender, object target)
        {
            ShowAnnouncePanel();
            m_HeaderText.text = "Game Over";
            m_ContentText.text = "Try it next time. Maybe you will have more luck.";
        }

        private void HideAnnouncePanel()
        {
            m_AnnouncePanel.SetActive(false);
        }

        private void ShowAnnouncePanel()
        {
            m_AnnouncePanel.SetActive(true);
        }
    }
}