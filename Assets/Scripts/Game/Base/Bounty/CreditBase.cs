using UnityEngine;
using Bachelor.MyExtensions;

namespace Bachelor.Game.Base
{
    public class CreditBase : MonoBehaviour
    {
        [SerializeField]
        private int m_Score = 10;

        public int Score
        {
            get
            {
                return (int)((m_ScaleWithSize ? transform.localScale.MagnitudeNormalize() : 1) * m_Score);                
            }
        }

        [SerializeField]
        private float m_QuestionChance = 0.2f;

        public float QuestionChance
        {
            get
            {
                return (m_ScaleWithSize ? transform.localScale.MagnitudeNormalize() : 1) * m_QuestionChance;
            }
        }

        [SerializeField]
        private bool m_ScaleWithSize = false;
    }
}