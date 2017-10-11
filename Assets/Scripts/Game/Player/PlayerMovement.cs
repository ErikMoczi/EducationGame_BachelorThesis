using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Bachelor.Game
{
    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float m_Speed = 5f;

        [SerializeField]
        private float m_Tilt = 5f;

        [SerializeField]
        private Boundary m_Boundary;

        private Rigidbody m_RigidBody;
        private float m_DefaultPositionY;
        private bool m_Moveflag = false;
        private Vector3 m_MovePosition;

        private void Awake()
        {
            m_RigidBody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            m_DefaultPositionY = transform.position.y;
        }

        private void FixedUpdate()
        {
            PerformMove();
        }

        private void PerformMove()
        {
            float moveHorizontal = -CrossPlatformInputManager.GetAxisRaw("Vertical");
            float moveVertical = CrossPlatformInputManager.GetAxisRaw("Horizontal");

            Vector3 movement = new Vector3(moveHorizontal, m_DefaultPositionY, moveVertical);
            m_RigidBody.velocity = movement * m_Speed;

            m_RigidBody.position = new Vector3(
                Mathf.Clamp(m_RigidBody.position.x, m_Boundary.xMin, m_Boundary.xMax),
                m_DefaultPositionY,
                Mathf.Clamp(m_RigidBody.position.z, m_Boundary.zMin, m_Boundary.zMax)
            );

            m_RigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, m_RigidBody.velocity.x * -m_Tilt);
        }
    }
}