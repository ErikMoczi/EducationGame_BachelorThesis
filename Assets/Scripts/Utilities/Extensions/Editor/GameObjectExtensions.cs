using UnityEngine;
using UnityEditor;

namespace Bachelor.MyExtensions.Editor
{
    public static class GameObjectExtensionsEditor
    {
        public static bool IsPrefabInstance(this GameObject gameObject)
        {
            return PrefabUtility.GetPrefabParent(gameObject) != null && PrefabUtility.GetPrefabObject(gameObject) != null;
        }

        public static bool IsPrefabOriginal(this GameObject gameObject)
        {
            return PrefabUtility.GetPrefabParent(gameObject) == null && PrefabUtility.GetPrefabObject(gameObject) != null;
        }

        public static bool IsDisconnectedPrefabInstance(this GameObject gameObject)
        {
            return PrefabUtility.GetPrefabParent(gameObject) != null && PrefabUtility.GetPrefabObject(gameObject) == null;
        }

        public static bool IsPrefabInstantiate(this GameObject gameObject)
        {
            return PrefabUtility.GetPrefabParent(gameObject) == null && PrefabUtility.GetPrefabObject(gameObject) == null;
        }

        public static bool IsPrefabInScene(this GameObject gameObject)
        {
            return gameObject.IsPrefabInstantiate() || gameObject.IsDisconnectedPrefabInstance();
        }
    }
}