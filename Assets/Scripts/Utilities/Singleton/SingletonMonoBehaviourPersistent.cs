using UnityEngine;

namespace Bachelor.Utilities
{
    public abstract class SingletonMonoBehaviourPersistent<T> : SingletonMonoBehaviour<T> where T : SingletonMonoBehaviour<T>
    {
        protected override void SetOnInstantiateCompleted()
        {
            base.SetOnInstantiateCompleted();
            OnInstantiateCompleted += SingletonMonoBehaviourPersistent_OnInstantiateCompleted;
        }

        private void SingletonMonoBehaviourPersistent_OnInstantiateCompleted(T instance)
        {
            OnInstantiateCompleted -= SingletonMonoBehaviourPersistent_OnInstantiateCompleted;
            if (IsInstantiated())
            {
                DontDestroyOnLoad(Instance);
                Debug.Log("[Singleton] Add GameObject '" + Instance.gameObject.name + "' as DontDestroyOnLoad.");
            }
        }
    }
}
