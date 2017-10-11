using Bachelor.MyExtensions.Managers;

namespace Bachelor.Game.Controllers.GM.Base
{
    public class EndingState : ExtendedState, INotification
    {
        public const string GameEndingNotification = "EndingState.GameEndingNotification";

        public override void Begin()
        {
            base.Begin();
            this.PostNotification(GameEndingNotification);
        }
    }
}