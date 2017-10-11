using UnityEditor;
using Bachelor.Utilities.Unity;
using Bachelor.Managers;

namespace Bachelor.EditorScripts
{
    [CustomEditor(typeof(TransitionSceneManager), true)]
    public class TransitionSceneManagerEditor : BaseEditor<TransitionSceneManager>
    {
        protected override void DrawOnlyForPrabInScene()
        {
            base.DrawOnlyForPrabInScene();

            EditorLayout.BeginVerticalBox();
            {
                EditorGUILayout.LabelField("Transition status", EditorStyles.boldLabel);

                EditorLayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Total transitions: " + CallObject.TransitionCount);
                }
                EditorLayout.EndHorizontal();

                /*
                if (CallObject.InTransition)
                {
                    string status = 
                        "RUNNING (" + 
                        (CallObject.Fading ? "Fading" : "Brightening") + 
                        ") - " + 
                        Mathf.RoundToInt(CallObject.TransitionPercent * 100) + 
                        "%"
                    ;
                    EditorLayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(status);
                    }
                    EditorLayout.EndHorizontal();        
                }   
                */
            }
            EditorLayout.EndVertical();
        }
    }
}