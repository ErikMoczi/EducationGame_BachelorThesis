using Bachelor.MyExtensions.Managers;

namespace Bachelor.Game.Controllers.GM.Base
{
    public class PlayingState : ExtendedState, INotification
    {
        public const string GameOverNotification = "PlayingState.GameOverNotification";

        public const string GameCompleteNotification = "PlayingState.GameCompleteNotification";

        public const string GamePlayingNotification = "PlayingState.GamePlayingNotification";

        private void OnEnable()
        {
            this.AddObserver(OnPlayerDeath, PlayerHealth.PlayerDeathNotification);
        }

        private void OnDisable()
        {
            this.RemoveObserver(OnPlayerDeath, PlayerHealth.PlayerDeathNotification);
        }        

        public override void Begin()
        {
            base.Begin();
            this.PostNotification(GamePlayingNotification);
        }

        private void OnPlayerDeath(object sender, object target)
        {
            if(sender is PlayerHealth)
            {
                this.PostNotification(GameOverNotification);
                GameController.Instance.ChangeState<EndingState>();
            }
        }
    }
}