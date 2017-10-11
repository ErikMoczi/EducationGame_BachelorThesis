using Bachelor.Game.Base;
using Bachelor.MyExtensions.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Bachelor.Game
{
    public class TrackHealth : TrackBase, INotification
    {
        [SerializeField]
        private Image m_DamageImage;

        [SerializeField]
        private Color m_StartingColor;

        [SerializeField]
        private Color m_EndingColor;

        [SerializeField]
        private Image m_HealthIndicator;

        [SerializeField]
        private float m_FlashSpeed = 5f;

        [SerializeField]
        private Color m_FlashColour = new Color(1f, 0f, 0f, 0.1f);

        private bool m_ProcessDamage = false;

        private void OnEnable()
        {
            this.AddObserver(OnUpdateHealthBar, PlayerHealth.PlayerTakeDamageNotification);
        }

        private void OnDisable()
        {
            this.RemoveObserver(OnUpdateHealthBar, PlayerHealth.PlayerTakeDamageNotification);
        }

        private void Start()
        {
            m_ProcessDamage = false;
            m_DamageImage.color = Color.clear;
            UpdateValue(1);
        }

        private void Update()
        {
            if (m_ProcessDamage)
            {
                if (m_DamageImage.color == Color.clear)
                {
                    m_DamageImage.color = m_FlashColour;
                }
                else
                {
                    m_DamageImage.color = Color.Lerp(m_DamageImage.color, Color.clear, m_FlashSpeed * Time.deltaTime);
                }

                if (m_DamageImage.color == Color.clear)
                {
                    m_ProcessDamage = false;
                }
            }
        }

        private void UpdateValue(float newValue)
        {
            m_HealthIndicator.fillAmount = newValue;
            m_HealthIndicator.color = Color.Lerp(m_EndingColor, m_StartingColor, m_HealthIndicator.fillAmount);
        }

        private void OnUpdateHealthBar(object sender, object target)
        {
            if(sender is PlayerHealth)
            {                
                PlayerHealth playerHealth = sender as PlayerHealth;
                UpdateValue((float)playerHealth.CurrentHealth / playerHealth.StartingHealth);
                StartProcessDamage();
            }
        }

        private void StartProcessDamage()
        {
            if (m_ProcessDamage)
            {
                m_ProcessDamage = false;
                m_DamageImage.color = Color.clear;
            }

            m_ProcessDamage = true;
        }
    }
}