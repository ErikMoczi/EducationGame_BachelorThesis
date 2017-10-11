using UnityEditor;
using UnityEngine;

namespace Bachelor.Utilities.Unity
{
    [CustomPropertyDrawer(typeof(ReadOnlyWhenPlayingAttribute))]
    public class ReadOnlyWhenPlayingAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = !Application.isPlaying;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}