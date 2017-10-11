using System.Reflection;
using System;

namespace Bachelor.Utilities
{
    public abstract class Singleton<T> where T : class
    {
        private static object m_LockObject = new object();
        private static bool m_ObjectIsLocked = false;
        private static T m_Instance;

        public static T Instance
        {
            get
            {
                if (m_SingletonDestroying)
                {
                    throw new Exception(
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
                        m_Instance = (T)Activator.CreateInstance(typeof(T), true);
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
                m_SingletonDestroying = true;
                HandleOnDestroyingDelegate();
                SetInstanceDefault();
            }
            m_SingletonDestroying = false;
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
    }
}