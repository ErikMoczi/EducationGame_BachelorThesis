using Bachelor.Game.Base;
using UnityEngine;

namespace Bachelor.Game.Controllers.GM.Base
{
    [RequireComponent(typeof(GameController))]
    public class ExtendedState : State
    {
        private GameController m_GameController;

        public GameController GameController
        {
            get
            {
                return m_GameController;
            }

            set
            {
                m_GameController = value;
            }
        }

        protected virtual void Awake()
        {
            GameController = GetComponent<GameController>();
        }
    }
}