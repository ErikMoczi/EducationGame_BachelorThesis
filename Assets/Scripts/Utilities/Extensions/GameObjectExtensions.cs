using UnityEngine;

namespace Bachelor.MyExtensions
{
    public static class GameObjectExtensions
    {
        static public T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T result = gameObject.GetComponent<T>();
            if (result == null)
            {
                result = gameObject.AddComponent<T>();
            }
            return result;
        }
    }
}