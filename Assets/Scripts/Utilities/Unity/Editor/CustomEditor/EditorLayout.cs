using UnityEngine;
using UnityEditor;

namespace Bachelor.Utilities.Unity
{
    public class EditorLayout
    {
        public static Rect BeginVertical()
        {
            return EditorGUILayout.BeginVertical();
        }

        public static Rect BeginVerticalBox(GUIStyle style = null)
        {
            return EditorGUILayout.BeginVertical(style ?? GUI.skin.box);
        }

        public static void EndVertical()
        {
            EditorGUILayout.EndVertical();
        }

        public static Rect BeginHorizontal()
        {
            return EditorGUILayout.BeginHorizontal();
        }

        public static void EndHorizontal()
        {
            EditorGUILayout.EndHorizontal();
        }
    }
}