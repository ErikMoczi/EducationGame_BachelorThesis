using Bachelor.Game.Base;
using Bachelor.Game.Controllers;
using Bachelor.MyExtensions.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Bachelor.Game
{
    public class TrackEnergy : TrackBase, INotification
    {
        [SerializeField]
        private Image m_EnergyIndicator;

        [SerializeField]
        private Text m_EnergyText;

        private void OnEnable()
        {
            this.AddObserver(OnUpdateEnergy, GameController.UpdateEnergyNotification);
        }

        private void OnDisable()
        {
            this.RemoveObserver(OnUpdateEnergy, GameController.UpdateEnergyNotification);
        }

        private void Start()
        {
            UpdateValue(0f);
        }

        private void OnUpdateEnergy(object sender, object target)
        {
            if (sender is GameController)
            {
                
                UpdateValue((sender as GameController).Energy);
            }
        }

        private void UpdateValue(float newValue)
        {
            m_EnergyIndicator.fillAmount = newValue;
            m_EnergyText.text = ((int)(newValue * 100)).ToString() + "%";
        }
    }
}