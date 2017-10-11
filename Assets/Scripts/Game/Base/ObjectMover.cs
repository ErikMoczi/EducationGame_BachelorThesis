using UnityEngine;

namespace Bachelor.Game.Base
{
    [RequireComponent(typeof(Rigidbody))]
    public class ObjectMover : MonoBehaviour
    {
        [SerializeField]
        private float m_Speed = 10f;

        private void Start()
        {
            GetComponent<Rigidbody>().velocity = transform.forward * m_Speed;
        }
    }
}