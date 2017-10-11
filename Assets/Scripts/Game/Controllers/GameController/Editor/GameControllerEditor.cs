using Bachelor.Game.Controllers;
using UnityEditor;

namespace Bachelor.EditorScripts
{
    [CustomEditor(typeof(GameController), true)]
    public class GameControllerEditor : BaseGameControllerBaseEditor
    {
        protected override void DrawOnlyForPrabInScene()
        {
            base.DrawOnlyForPrabInScene();
        }
    }
}