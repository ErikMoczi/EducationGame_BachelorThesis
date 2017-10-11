using Bachelor.Managers;
using Bachelor.Managers.XML;
using Bachelor.SerializeData;
using Bachelor.SerializeData.PlanetInfo;
using UnityEngine;
using UnityEngine.UI;

namespace Bachelor.Planets
{
    public class PlanetInfoLoader : MonoBehaviour
    {
        [SerializeField]
        private Text m_HeaderDisplay;

        [SerializeField]
        private Text m_TextDisplay;

        [SerializeField]
        private Slider m_SliderPlanet;

        [SerializeField]
        private MeshRenderer m_RenderPlanet;

        [SerializeField]
        private PlanetMaterialList m_PlanetMaterialList;

        private int m_DisplayPlanet = 3;
        private bool m_ChangedPlanet;
        private PlanetContainer m_PlanetContainer;

        private void Start()
        {
            m_PlanetContainer = XMLManager.Instance.PlanetContainer;
            m_ChangedPlanet = true;
            m_SliderPlanet.value = m_DisplayPlanet;
            DisplayInfoPlanet();
        }

        private void Update()
        {
            if (m_ChangedPlanet)
            {
                DisplayInfoPlanet();
                m_ChangedPlanet = false;
            }
        }

        public void UpdateDisplayIdPlanet()
        {
            m_DisplayPlanet = (int)m_SliderPlanet.value;
            m_ChangedPlanet = true;
        }

        private void DisplayInfoPlanet()
        {
            m_RenderPlanet.material = FindMaterialPlanetById(m_DisplayPlanet);
            DisplayPlanetText();
        }

        private Material FindMaterialPlanetById(int findId)
        {
            return m_PlanetMaterialList.DetailsList.Find(x => x.Id == findId).Material;
        }

        private void DisplayPlanetText()
        {
            Planet planet = m_PlanetContainer.FindPlanet(m_DisplayPlanet);
            if (planet != null)
            {
                SetHeaderDisplay(planet.Name);

                string newText = "";
                foreach (string line in planet.TextsLine)
                {
                    newText += line;
                    if (planet.TextsLine.IndexOf(line) < planet.TextsLine.Count)
                    {
                        newText += "\n";
                    }
                }
                SetTextDisplay(newText);
            }
            else
            {
                SetHeaderDisplay("Chyba");
                SetTextDisplay("");
            }
        }

        private void SetHeaderDisplay(string newValue)
        {
            m_HeaderDisplay.text = newValue;
        }

        private void SetTextDisplay(string newValue)
        {
            m_TextDisplay.text = newValue;
        }
    }
}