using UnityEngine;
using UnityEditor;
using Bachelor.Utilities.Unity;
using Bachelor.Managers.Transition.Base;
using Bachelor.MyExtensions.Managers;

namespace Bachelor.EditorScripts
{
    [CustomEditor(typeof(TransitionElementBase), true)]
    public class TransitionElementBaseEditor : BaseEditor<TransitionElementBase>
    {
        protected override void DrawOnlyForPrabInScene()
        {
            base.DrawOnlyForPrabInScene();

            if (this.CallObject.InTransitionSafe())
            {
                EditorLayout.BeginVerticalBox();
                {
                    string status =
                        "RUNNING (" +
                        (CallObject.GetTransitionStatus().TransitionFading ? "Fading" : "Brightening") +
                        ") - " +
                        Mathf.RoundToInt(CallObject.GetTransitionStatus().TransitionPercent * 100) +
                        "%"
                    ;
                    EditorLayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(status);
                    }
                    EditorLayout.EndHorizontal();
                }
                EditorLayout.EndVertical();
            }
        }
    }
}