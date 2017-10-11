using Bachelor.Game.Controllers.GM.Base;
using Bachelor.MyExtensions.Managers;
using UnityEngine;

namespace Bachelor.Game.Base
{
    public class GameObjectController : MonoBehaviour, INotification
    {
        protected virtual void OnEnable()
        {
            this.AddObserver(OnGamePlaying, PlayingState.GamePlayingNotification);
            this.AddObserver(OnGameEnding, EndingState.GameEndingNotification);
        }

        protected virtual void OnDisable()
        {
            this.RemoveObserver(OnGamePlaying, PlayingState.GamePlayingNotification);
            this.RemoveObserver(OnGameEnding, EndingState.GameEndingNotification);
        }

        private void OnGameEnding(object arg1, object arg2)
        {
            gameObject.SetActive(false);
        }

        private void OnGamePlaying(object arg1, object arg2)
        {
            gameObject.SetActive(true);
        }
    }
}