using UnityEngine;

namespace Bachelor.MyExtensions
{
    public static class ComponentExtensions
    {
        static public T GetOrAddComponent<T>(this Component component) where T : Component
        {
            T result = component.GetComponent<T>();
            if (result == null)
            {
                result = component.gameObject.AddComponent<T>();
            }
            return result;
        }
    }
}