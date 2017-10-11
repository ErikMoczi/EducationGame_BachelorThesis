using UnityEngine;
using UnityEditor;
using Bachelor.Utilities.Unity;
using Bachelor.Utilities;
using Bachelor.Managers;

namespace Bachelor.EditorScripts
{
    [CustomEditor(typeof(MusicManager), true)]
    public class MusicManagerEditor : BaseEditor<MusicManager>
    {
        protected override void DrawOnlyForPrabInScene()
        {
            base.DrawOnlyForPrabInScene();

            AudioClip currentAudioClip = CallObject.CurrentAudioClip;

            EditorLayout.BeginVerticalBox();
            {
                EditorGUILayout.LabelField("Music manager", EditorStyles.boldLabel);

                EditorLayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Total songs: " + CallObject.MusicPlaylistList.Count);
                }
                EditorLayout.EndHorizontal();

                EditorLayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Current song: " + currentAudioClip.name);
                }
                EditorLayout.EndHorizontal();

                EditorLayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField(Utility.ConvertTimeToString(CallObject.AudioSource.time) + " / " + Utility.ConvertTimeToString(currentAudioClip.length));
                }
                EditorLayout.EndHorizontal();

                EditorLayout.BeginHorizontal();
                {
                    var buttonStyle = new GUIStyle(GUI.skin.button);
                    if (GUILayout.Button("Play", buttonStyle, GUILayout.Width(50)))
                    {
                        CallObject.Play();
                    }
                    if (GUILayout.Button(CallObject.IsPaused() ? "UnPause" : "Pause", buttonStyle, GUILayout.Width(60)))
                    {
                        CallObject.Pause();
                    }
                    if (GUILayout.Button("Next", buttonStyle, GUILayout.Width(50)))
                    {
                        CallObject.Next();
                    }
                    if (GUILayout.Button("Stop", buttonStyle, GUILayout.Width(50)))
                    {
                        CallObject.Stop();
                    }
                }
                EditorLayout.EndHorizontal();
            }
            EditorLayout.EndVertical();
        }
    }
}