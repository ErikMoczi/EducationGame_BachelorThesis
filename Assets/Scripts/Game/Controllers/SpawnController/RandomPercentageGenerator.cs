using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bachelor.Game.Controllers.Spawn.Base
{
    [Serializable]
    public class RandomPercentageGenerator
    {
        private const float m_defaultChance = 1f;

        [SerializeField]
        private SpawnGameObjectPercentage[] m_SpawnGameObjects;

        [SerializeField]
        private SpawnGameObject m_DefaultSpawnGameObject;

        public GameObject GetItem()
        {
            GameObject gameObject = m_DefaultSpawnGameObject.GameObject;
            float randomChance = Random.Range(0f, Mathf.Max(m_defaultChance, m_SpawnGameObjects.Sum(x => x.SpawnChance)));
            foreach (var item in m_SpawnGameObjects)
            {
                randomChance -= item.SpawnChance;
                if (randomChance <= 0)
                {
                    return item.GameObject;
                }
            }
            return gameObject;
        }
    }
}