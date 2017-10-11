using System;
using System.Reflection;
using UnityEngine;

namespace Bachelor.Utilities
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static readonly object m_LockObject = new object();
        private static bool m_ObjectIsLocked = false;
        private static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_SingletonDestroying || m_ApplicationIsQuitting)
                {
                    Debug.LogWarning(
                        "[Singleton] Instance '" + typeof(T) + 
                        "' already destroyed." +
                        " Create new one - returning null."
                    );
                    return null;
                }
                if (!IsInstantiated())
                {
                    Instantiate();
                }
                return m_Instance;
            }
        }

        private static bool m_SingletonDestroying = false;
        private static bool m_ApplicationIsQuitting = false;

        public delegate void OnInstantiateCompletedDelegate(T instance);
        public static OnInstantiateCompletedDelegate OnInstantiateCompleted;

        public delegate void OnDestroyingDelegate(T instance);
        public static OnDestroyingDelegate OnDestroying;        

        public static T Instantiate()
        {
            if (!IsInstantiated())
            {
                lock (m_LockObject)
                {
                    m_ObjectIsLocked = true;
                    if (!IsInstantiated())
                    {
                        ConstructorTest();
                        m_Instance = (T)FindObjectOfType(typeof(T));
                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError(
                                "[Singleton] Something went really wrong - there should never be more than 1 singleton!" +
                                " Reopening the scene might fix it."
                            );
                            return m_Instance;
                        }

                        if (m_Instance == null)
                        {
                            T[] prefabObjects = Resources.FindObjectsOfTypeAll<T>();
                            if (prefabObjects.Length > 0)
                            {
                                if (prefabObjects.Length == 1)
                                {
                                    m_Instance = Instantiate(prefabObjects[0]) as T;
                                }
                                else
                                {
                                    Debug.LogWarning(
                                        "[Singleton] Instance '" + typeof(T) +
                                        "' got too many prefabs in Resources. It will be create by default."
                                    );
                                }
                            }

                            if(m_Instance == null)
                            {
                                GameObject singleton = new GameObject();
                                m_Instance = singleton.AddComponent<T>();
                                singleton.name = "(singleton) " + typeof(T);
                                Debug.Log(
                                    "[Singleton] An instance of " + typeof(T) +
                                    " is needed in the scene, so '" + singleton +
                                    "' was created."
                                );
                            }                            
                        }
                        else
                        {
                            Debug.Log("[Singleton] Using instance already created: " + m_Instance.gameObject.name);
                        }
                        HandleOnInstantiateCompletedDelegate();                        
                    }
                }
                m_ObjectIsLocked = false;
            }
            return m_Instance;
        }

        public static bool IsInstantiated()
        {
            return m_Instance != null;
        }

        public static void Destroy()
        {
            if (IsInstantiated())
            {
                DestroyImmediate(m_Instance.gameObject);
                SetInstanceDefault();
            }
        }

        protected virtual void Awake()
        {
            SetOnInstantiateCompleted();
            if (!m_ObjectIsLocked)
            {                
                if (!IsInstantiated())
                {
                    Instantiate();
                }
                else if(!IsSameGameObject())
                {                    
                    Debug.LogWarning(
                        "[Singleton] There should never be more than 1 singleton!" +
                        " Destroying the bad one - '" + gameObject + "'."
                    );
                    Destroy(gameObject);
                }
            }
        }

        protected virtual void SetOnInstantiateCompleted()
        {
        }

        protected virtual void OnDestroy()
        {            
            if (IsInstantiated() && IsSameGameObject())
            {
                m_SingletonDestroying = true;
                HandleOnDestroyingDelegate();
                SetInstanceDefault();
            }
            m_SingletonDestroying = false;
        }

        protected virtual void OnApplicationQuit()
        {
            m_ApplicationIsQuitting = true;
        }

        private static void ConstructorTest()
        {
            ConstructorInfo constructor = null;
            try
            {
                constructor = typeof(T).GetConstructor(
                    BindingFlags.Instance |
                    BindingFlags.NonPublic,
                    null,
                    Type.EmptyTypes,
                    null
                );
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            if (constructor == null || constructor.IsAssembly)
            {
                throw new NotPrivateConstructor(typeof(T).ToString());
            }
        }

        private static void HandleOnInstantiateCompletedDelegate()
        {
            if (OnInstantiateCompleted != null)
            {
                OnInstantiateCompleted(m_Instance);
            }
        }

        private static void HandleOnDestroyingDelegate()
        {
            if (OnDestroying != null)
            {
                OnDestroying(m_Instance);
            }
        }

        private static void SetInstanceDefault()
        {
            m_Instance = null;
        }

        private bool IsSameGameObject()
        {
            return m_Instance.gameObject.Equals(gameObject);
        }
    }
}