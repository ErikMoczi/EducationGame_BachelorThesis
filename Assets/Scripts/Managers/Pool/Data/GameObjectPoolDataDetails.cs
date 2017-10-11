using Bachelor.Utilities;
using System;
using UnityEngine;

namespace Bachelor.SerializeData
{
    [Serializable]
    public class GameObjectPoolDataDetails : SODetails
    {
        [SerializeField]
        private string poolName;

        public string PoolName
        {
            get
            {
                if (string.IsNullOrEmpty(poolName))
                {
                    poolName = m_PrefabGameObject.name;
                }
                return poolName;
            }
        }

        [SerializeField]
        private GameObject m_PrefabGameObject;

        public GameObject PrefabGameObject
        {
            get
            {
                return m_PrefabGameObject;
            }
        }

        [SerializeField]
        private int m_PoolSize = 5;

        public int PoolSize
        {
            get
            {
                return m_PoolSize;
            }

            set
            {
                m_PoolSize = value;
            }
        }

        [SerializeField]
        private bool m_FixedSize = false;

        public bool FixedSize
        {
            get
            {
                return m_FixedSize;
            }
        }
    }
}