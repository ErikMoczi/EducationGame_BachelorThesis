using Bachelor.Managers.Pool.Base;
using Bachelor.SerializeData;
using Bachelor.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bachelor.Managers
{
    public class GameObjectPool : SingletonMonoBehaviour<GameObjectPool>
    {
        [SerializeField]
        private GameObjectPoolDataList[] m_GameObjectPoolDataList;

        [SerializeField]
        private Transform m_BaseView;

        private Dictionary<string, PoolData> m_PoolTable = new Dictionary<string, PoolData>();

        private GameObjectPool() { }

        protected override void Awake()
        {
            base.Awake();
            InitPoolTable();
        }

        public void CreatePool(GameObjectPoolDataDetails gameObjectPoolDataDetails, Transform transformView)
        {
            if(gameObjectPoolDataDetails != null)
            {
                if (!m_PoolTable.ContainsKey(gameObjectPoolDataDetails.PoolName))
                {
                    PoolData poolData = new PoolData(gameObjectPoolDataDetails, transformView);
                    m_PoolTable.Add(gameObjectPoolDataDetails.PoolName, poolData);
                }
                else
                {
                    Debug.LogWarning("[GameObjectPool] Pool with name '" + gameObjectPoolDataDetails.PoolName + "' already exists.");
                }
            }
            else
            {
                Debug.LogError("[GameObjectPool] PoolDataDetails is null");
            }
        }

        public GameObject Spawn(string poolName, Vector3 position, Quaternion rotation, float lifeTime = 0)
        {
            GameObject gameObject = null;

            if (m_PoolTable.ContainsKey(poolName))
            {
                PoolData poolData = m_PoolTable[poolName];
                PoolObject poolObject = poolData.Get();

                if(poolObject != null)
                {
                    poolObject.LifeTime = lifeTime;
                    Transform transform = poolObject.transform;
                    transform.localPosition = position;
                    transform.localRotation = rotation;
                    gameObject = poolObject.gameObject;
                }
            }
            else
            {
                Debug.LogWarning("[GameObjectPool] Invalid pool name '" + poolName + "'.");
            }

            return gameObject;
        }

        public GameObject Spawn(string poolName, Vector3 position)
        {
            return Spawn(poolName, position, Quaternion.identity);
        }

        public GameObject Spawn(string poolName, Quaternion rotation)
        {
            return Spawn(poolName, Vector3.zero, rotation);
        }

        public GameObject Spawn(string poolName)
        {
            return Spawn(poolName, Vector3.zero, Quaternion.identity);
        }

        public void Despawn(GameObject gameObject)
        {
            PoolObject poolObject = gameObject.GetComponent<PoolObject>();

            if(poolObject != null)
            {
                if (m_PoolTable.ContainsKey(poolObject.PoolName))
                {
                    PoolData poolData = m_PoolTable[poolObject.PoolName];
                    poolData.Push(poolObject);

                }
                else
                {
                    Debug.LogWarning("[GameObjectPool] Invalid pool name '" + poolObject.PoolName + "'.");
                }
            }
            else
            {
                Debug.LogWarning("[GameObjectPool] GameObject is not a pooled component: " + gameObject.name);
            }
        }

        public string FindPoolName(GameObject gameObject)
        {
            return m_PoolTable.FirstOrDefault(x => x.Value.PrefabGameObject.Equals(gameObject)).Key;
        }

        private void InitPoolTable()
        {
            if(m_GameObjectPoolDataList == null)
            {
                Debug.LogWarning("[GameObjectPool] There are no pooling objects!");
                return;
            }

            foreach(GameObjectPoolDataList gameObjectPoolDataList in m_GameObjectPoolDataList)
            {
                Transform transformView = m_BaseView.Find(gameObjectPoolDataList.TransformView);

                if(transformView == null)
                {
                    transformView = new GameObject(gameObjectPoolDataList.TransformView).transform;
                    transformView.SetParent(m_BaseView);
                }

                foreach(GameObjectPoolDataDetails gameObjectPoolDataDetails in gameObjectPoolDataList)
                {
                    CreatePool(gameObjectPoolDataDetails, transformView);
                }
            }
        }
    }
}