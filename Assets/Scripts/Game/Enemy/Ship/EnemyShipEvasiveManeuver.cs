using UnityEngine;
using System.Collections;

namespace Bachelor.Game
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyShipEvasiveManeuver : MonoBehaviour
    {
        private const float m_DefaultPositionY = 0.0f;

        [SerializeField]
        public Boundary m_Boundary;

        [SerializeField]
        private float m_Tilt = 10f;

        [SerializeField]
        private float m_Dodge = 5f;

        [SerializeField]
        private float m_Smoothing = 7.5f;

        [SerializeField]
        private Vector2 m_StartWait = new Vector2(0.5f, 1f);

        [SerializeField]
        private Vector2 m_ManeuverTime = new Vector2(1f, 2f);

        [SerializeField]
        private Vector2 m_ManeuverWait = new Vector2(1f, 2f);

        private float m_CurrentSpeed;
        private float m_TargetManeuver;

        private void Start()
        {
            m_CurrentSpeed = GetComponent<Rigidbody>().velocity.z;            
        }

        private void OnEnable()
        {
            StartCoroutine(Evade());
        }

        private void FixedUpdate()
        {
            PerformMove();
        }

        private void PerformMove()
        {
            m_CurrentSpeed = GetComponent<Rigidbody>().velocity.z;
            
            float newManeuver = Mathf.MoveTowards(GetComponent<Rigidbody>().velocity.x, m_TargetManeuver, m_Smoothing * Time.deltaTime);
            GetComponent<Rigidbody>().velocity = new Vector3(newManeuver, 0.0f, m_CurrentSpeed);
            GetComponent<Rigidbody>().position = new Vector3(
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, m_Boundary.xMin, m_Boundary.xMax),
                m_DefaultPositionY,
                GetComponent<Rigidbody>().position.z
                );

            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -m_Tilt);
        }

        private IEnumerator Evade()
        {
            yield return new WaitForSeconds(Random.Range(m_StartWait.x, m_StartWait.y));
            while (true)
            {
                yield return null;
                m_TargetManeuver = Random.Range(1, m_Dodge) * -Mathf.Sign(transform.position.x);
                yield return new WaitForSeconds(Random.Range(m_ManeuverTime.x, m_ManeuverTime.y));
                m_TargetManeuver = 0;
                yield return new WaitForSeconds(Random.Range(m_ManeuverWait.x, m_ManeuverWait.y));
            }
        }
    }
}