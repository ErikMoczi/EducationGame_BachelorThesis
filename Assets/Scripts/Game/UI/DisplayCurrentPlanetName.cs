using Bachelor.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Bachelor.Game
{
    public class DisplayCurrentPlanetName : MonoBehaviour
    {
        [SerializeField]
        private Text m_LevelText;

        private void Start()
        {
            m_LevelText.text = XMLManager.Instance.QuestionContainer.CurrentPlanet.Name;
        }
    }
}