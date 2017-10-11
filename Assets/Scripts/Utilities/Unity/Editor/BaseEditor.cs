using Bachelor.MyExtensions.Editor;
using UnityEditor;
using UnityEngine;

namespace Bachelor.Utilities.Unity
{
    public abstract class BaseEditor<T> : Editor where T : MonoBehaviour
    {
        private T m_CallObject;

        protected T CallObject
        {
            get
            {
                return m_CallObject;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorUtility.SetDirty(target);
            m_CallObject = (T)target;
            if (m_CallObject.gameObject.IsPrefabInScene() && Application.isPlaying)
            {
                DrawOnlyForPrabInScene();
            }
        }

        protected virtual void DrawOnlyForPrabInScene()
        {
        }
    }
}