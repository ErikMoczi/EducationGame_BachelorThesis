using System;
using UnityEngine;

namespace Bachelor.Game.Controllers.Spawn.Base
{
    [Serializable]
    public class SpawnGameObject
    {
        [SerializeField]
        private GameObject m_GameObject;

        public GameObject GameObject
        {
            get
            {
                return m_GameObject;
            }
        }
    }
}