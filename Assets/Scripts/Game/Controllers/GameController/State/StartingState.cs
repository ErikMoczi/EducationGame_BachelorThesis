using Bachelor.MyExtensions.Managers;
using System.Collections;

namespace Bachelor.Game.Controllers.GM.Base
{
    public class StartingState : ExtendedState, INotification
    {
        public const string GameStartingNotification = "StartingState.GameStartingNotification";

        public override void Begin()
        {
            base.Begin();
            this.PostNotification(GameStartingNotification);
            StartCoroutine(Starting());
        }

        private IEnumerator Starting()
        {
            yield return null;
            GameController.ChangeState<PlayingState>();            
        }
    }
}