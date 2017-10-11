using UnityEngine;

namespace Bachelor.Game
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField]
        private float m_ScrollSpeed = -2f;

        [SerializeField]
        private float m_TileSizeZ = 100f;

        private Vector3 m_StartPosition;

        private void Start()
        {
            m_StartPosition = transform.position;
        }

        private void FixedUpdate()
        {
            float newPosition = Mathf.Repeat(Time.time * m_ScrollSpeed, m_TileSizeZ);
            transform.position = m_StartPosition + Vector3.forward * newPosition;
        }
    }
}