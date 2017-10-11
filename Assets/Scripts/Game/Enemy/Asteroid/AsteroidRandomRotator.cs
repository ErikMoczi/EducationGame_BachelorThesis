using UnityEngine;

namespace Bachelor.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class AsteroidRandomRotator : MonoBehaviour
    {
        [SerializeField]
        public float m_Tumble = 5f;

        private void Start()
        {
            GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * m_Tumble;
        }
    }
}