using Bachelor.Game.Base;
using UnityEngine;
using UnityEditor;
using Bachelor.Utilities.Unity;

namespace Bachelor.EditorScripts
{
    [CustomEditor(typeof(GameControllerBase), true)]
    public class BaseGameControllerBaseEditor : BaseEditor<GameControllerBase>
    {
        protected override void DrawOnlyForPrabInScene()
        {
            base.DrawOnlyForPrabInScene();

            EditorLayout.BeginVerticalBox();
            {
                EditorLayout.BeginHorizontal();
                {
                    string status = CallObject.Pause ? "Resume" : "Pause";
                    var buttonStyle = new GUIStyle(GUI.skin.button);

                    if (GUILayout.Button(status, buttonStyle, GUILayout.Width(50)))
                    {
                        CallObject.Pause = !CallObject.Pause;
                    }

                    if (GUILayout.Button("Restart", buttonStyle, GUILayout.Width(50)))
                    {
                        CallObject.RestartCurrentLevel();
                    }
                }
                EditorLayout.EndHorizontal();
            }
            EditorLayout.EndVertical();
        }
    }
}