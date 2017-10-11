using Bachelor.Managers;
using UnityEngine;

namespace Bachelor.MyExtensions.Managers
{
    public static class IGameObjectPoolExtensions
    {
        public static GameObject Spawn(this IGameObjectPool obj, string poolName, Vector3 position, Quaternion rotation, float lifeTime)
        {
            return GameObjectPool.Instance.Spawn(poolName, position, rotation, lifeTime);
        }

        public static GameObject Spawn(this IGameObjectPool obj, string poolName, Vector3 position, Quaternion rotation)
        {
            return GameObjectPool.Instance.Spawn(poolName, position, rotation);
        }

        public static GameObject Spawn(this IGameObjectPool obj, string poolName, Vector3 position)
        {
            return GameObjectPool.Instance.Spawn(poolName, position);
        }

        public static GameObject Spawn(this IGameObjectPool obj, string poolName)
        {
            return GameObjectPool.Instance.Spawn(poolName);
        }

        public static void Despawn(this IGameObjectPool obj, GameObject gameObject)
        {
            GameObjectPool.Instance.Despawn(gameObject);
        }

        public static string FindPoolName(this IGameObjectPool obj, GameObject gameObject)
        {
            return GameObjectPool.Instance.FindPoolName(gameObject);
        }
    }
}