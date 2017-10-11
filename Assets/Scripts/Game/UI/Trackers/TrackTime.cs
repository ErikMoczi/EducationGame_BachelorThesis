using Bachelor.Game.Base;
using Bachelor.Game.Controllers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bachelor.Game
{
    public class TrackTime : TrackBase
    {
        [SerializeField]
        private Image m_TimeIndicator;

        [SerializeField]
        private Color m_StartingColor;

        [SerializeField]
        private Color m_EndingColor;

        [SerializeField]
        private Text m_TimeNumber;

        [SerializeField]
        private GameController m_GameController;

        private void Update()
        {            
            m_TimeIndicator.fillAmount = (float)m_GameController.PlayingTimeElapsed / m_GameController.MaxPlayingTime;
            m_TimeIndicator.color = Color.Lerp(m_EndingColor, m_StartingColor, m_TimeIndicator.fillAmount);
            m_TimeNumber.text = CalculateTimeNumber(m_GameController.PlayingTimeElapsed).ToString();
        }

        private string CalculateTimeNumber(int newTime)
        {
            TimeSpan time = TimeSpan.FromSeconds(newTime);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", (int)time.TotalHours, time.Minutes, time.Seconds);
        }
    }
}