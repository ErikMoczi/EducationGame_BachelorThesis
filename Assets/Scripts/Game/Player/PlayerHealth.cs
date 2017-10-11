using Bachelor.Game.Base;
using Bachelor.MyExtensions.Managers;

namespace Bachelor.Game
{
    public class PlayerHealth : GameObjectHealth, INotification
    {
        public const string PlayerDeathNotification = "PlayerHealth.PlayerDeathNotification";

        public const string PlayerTakeDamageNotification = "PlayerHealth.PlayerTakeDamageNotification";

        protected override void OnTakeDamage()
        {
            base.OnTakeDamage();
            this.PostNotification(PlayerTakeDamageNotification);
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            this.PostNotification(PlayerDeathNotification);
        }
    }
}