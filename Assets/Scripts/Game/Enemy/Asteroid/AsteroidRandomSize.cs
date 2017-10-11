using UnityEngine;

namespace Bachelor.Game
{
    [System.Serializable]
    public class AsteroidScale
    {
        public float minScale, maxScale;
    }

    public class AsteroidRandomSize : MonoBehaviour
    {
        [SerializeField]
        private AsteroidScale m_AsteroidScale;

        private void Start()
        {
            float randomScale = Random.Range(m_AsteroidScale.minScale, m_AsteroidScale.maxScale);
            transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }
}