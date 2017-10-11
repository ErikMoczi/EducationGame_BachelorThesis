using Bachelor.MyExtensions;
using Bachelor.SerializeData;
using System.Collections.Generic;
using UnityEngine;

namespace Bachelor.Managers.Pool.Base
{
    public class PoolData
    {
        public GameObject PrefabGameObject
        {
            get
            {
                return m_GameObjectPoolDataDetails.PrefabGameObject;
            }
        }

        private Stack<PoolObject> m_StackObject = new Stack<PoolObject>();
        private GameObjectPoolDataDetails m_GameObjectPoolDataDetails;
        private Transform m_TransformParent;

        public PoolData(GameObjectPoolDataDetails gameObjectPoolDataDetails, Transform transformParent)
        {
            m_GameObjectPoolDataDetails = gameObjectPoolDataDetails;
            m_TransformParent = transformParent;
            Init();
        }

        public PoolObject Get()
        {
            PoolObject poolObject = null;

            if(m_StackObject.Count > 0)
            {
                poolObject = m_StackObject.Pop();
            }
            else if(m_GameObjectPoolDataDetails.FixedSize == false)
            {
                m_GameObjectPoolDataDetails.PoolSize++;
                poolObject = InstantiatePrefab();
            }
            else
            {
                Debug.LogWarning("No object available to be pooled for pool " + m_GameObjectPoolDataDetails.PoolName);
            }

            if(poolObject != null)
            {
                poolObject.IsPooled = false;
                poolObject.gameObject.SetActive(true);
            }

            return poolObject;
        }

        public void Push(PoolObject poolObject)
        {
            if (m_GameObjectPoolDataDetails.PoolName.Equals(poolObject.PoolName))
            {
                if (m_StackObject.Contains(poolObject))
                {
                    Debug.LogWarning("Trying to return already pooled object '" + poolObject.gameObject.name + "' to pool.");
                }
                else
                {
                    AddObjectToPool(poolObject);
                }
            }
            else
            {
                Debug.LogError(
                    "Trying to add object '" + poolObject.gameObject.name +
                    "' to incorrect pool (" + poolObject.PoolName + 
                    " | " + m_GameObjectPoolDataDetails.PoolName + "."
                );
            }
        }

        private void Init()
        {
            for (int i = 0; i < m_GameObjectPoolDataDetails.PoolSize; i++)
            {
                AddObjectToPool(InstantiatePrefab());
            }
        }

        private PoolObject InstantiatePrefab()
        {
            GameObject gameObject = Object.Instantiate(m_GameObjectPoolDataDetails.PrefabGameObject);
            PoolObject poolObject = gameObject.GetOrAddComponent<PoolObject>() as PoolObject;            
            DefaultSetUp(poolObject);
            return poolObject;
        }

        private void AddObjectToPool(PoolObject poolObject)
        {
            poolObject.gameObject.SetActive(false);
            m_StackObject.Push(poolObject);            
            poolObject.IsPooled = true;
        }

        private void DefaultSetUp(PoolObject poolObject)
        {
            poolObject.transform.SetParent(m_TransformParent);
            poolObject.PoolName = m_GameObjectPoolDataDetails.PoolName;
            poolObject.LifeTime = 0f;
            poolObject.IsPooled = false;
        }
    }
}