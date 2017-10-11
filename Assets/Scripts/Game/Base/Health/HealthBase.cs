using Bachelor.MyExtensions.Managers;
using UnityEngine;

namespace Bachelor.Game.Base
{
    public abstract class HealthBase : MonoBehaviour, IDamageAble, INotification
    {
        public const string ObjectDeathNotification = "HealthBase.ObjectDeathNotification";

        public const string ObjectTakeDamageNotification = "HealthBase.ObjectTakeDamageNotification";

        [SerializeField]
        private int m_Startinghealth = 100;

        public virtual int StartingHealth
        {
            get
            {
                return m_Startinghealth;
            }
        }

        [SerializeField]
        private int m_CurrentHealth;

        public virtual int CurrentHealth
        {
            get
            {
                return m_CurrentHealth;
            }

            protected set
            {
                if (value < 0)
                {
                    value = 0;
                }
                m_CurrentHealth = value;
            }
        }

        private bool m_IsDead = false;

        public bool IsDead
        {
            get
            {
                return m_IsDead;
            }
        }

        private bool m_TakeDamage = false;

        protected virtual void Start()
        {
            CurrentHealth = m_Startinghealth;
            m_IsDead = false;
            m_TakeDamage = false;
        }

        protected virtual void OnTakeDamage()
        {
            this.PostNotification(ObjectTakeDamageNotification);
        }

        public void TakeDamage(int amount)
        {
            CurrentHealth -= amount;
            OnTakeDamage();
            if (CurrentHealth <= 0 && !m_IsDead)
            {
                Death();
            }            
        }

        protected virtual void OnDeath()
        {
            this.PostNotification(ObjectDeathNotification);
        }

        private void Death()
        {
            m_IsDead = true;
            OnDeath();
            Destroy(gameObject);
        }
    }
}