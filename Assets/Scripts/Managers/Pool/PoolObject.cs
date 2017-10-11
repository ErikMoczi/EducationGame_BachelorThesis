using Bachelor.MyExtensions.Managers;
using Bachelor.Utilities.Unity;
using System.Collections;
using UnityEngine;

namespace Bachelor.Managers.Pool.Base
{
    public class PoolObject : MonoBehaviour, IGameObjectPool
    {
        [SerializeField]
        [ReadOnlyWhenPlaying]
        private string m_PoolName;

        public string PoolName
        {
            get
            {
                return m_PoolName;
            }

            set
            {
                m_PoolName = value;
            }
        }

        [SerializeField]
        [ReadOnlyWhenPlaying]
        private bool m_IsPooled = false;

        public bool IsPooled
        {
            get
            {
                return m_IsPooled;
            }

            set
            {
                m_IsPooled = value;
            }
        }

        [SerializeField]
        [ReadOnlyWhenPlaying]
        private float m_lifeTime = 0f;

        public float LifeTime
        {
            get
            {
                return m_lifeTime;
            }

            set
            {
                m_lifeTime = value;
                LifeTimeBehaviour();
            }
        }

        private IEnumerator m_Coroutine;

        private void OnEnable()
        {
            LifeTimeBehaviour();
        }

        private void OnDisable()
        {
            if(m_Coroutine != null)
            {
                StopCoroutine(m_Coroutine);
            }            
        }

        public void Despawn()
        {
            this.Despawn(gameObject);
        }

        private void LifeTimeBehaviour()
        {
            if (m_lifeTime > 0f && m_Coroutine == null)
            {
                m_Coroutine = AutoDestroy(m_lifeTime);
                StartCoroutine(m_Coroutine);
            }
        }

        private IEnumerator AutoDestroy(float time)
        {
            yield return new WaitForSeconds(time);
            Despawn();
        }
    }
}