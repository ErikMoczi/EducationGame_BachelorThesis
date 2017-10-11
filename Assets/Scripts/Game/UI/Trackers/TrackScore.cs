using Bachelor.Game.Base;
using Bachelor.Game.Controllers;
using Bachelor.MyExtensions.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Bachelor.Game
{
    public class TrackScore : TrackBase, INotification
    {
        [SerializeField]
        private Text m_ScoreText;        

        private void OnEnable()
        {
            this.AddObserver(OnUpdateScore, GameController.UpdateScoreNotification);
        }

        private void OnDisable()
        {
            this.RemoveObserver(OnUpdateScore, GameController.UpdateScoreNotification);
        }

        private void OnUpdateScore(object sender, object target)
        {
            if(sender is GameController)
            {
                m_ScoreText.text = (sender as GameController).Score.ToString();
            }
        }
    }
}