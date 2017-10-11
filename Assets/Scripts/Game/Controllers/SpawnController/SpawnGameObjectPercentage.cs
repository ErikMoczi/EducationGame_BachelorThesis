using System;
using UnityEngine;

namespace Bachelor.Game.Controllers.Spawn.Base
{
    [Serializable]
    public class SpawnGameObjectPercentage : SpawnGameObject
    {
        [SerializeField]
        private float m_SpawnChance = 1f;

        public float SpawnChance
        {
            get
            {
                return m_SpawnChance;
            }
        }
    }
}