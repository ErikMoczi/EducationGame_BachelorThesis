using Bachelor.MyExtensions;
using UnityEngine;

namespace Bachelor.Game.Base
{
    public class GameObjectDestroyByContact : ObjectDestroyByContactBase
    {
        [SerializeField]
        private int m_DamageValue = 1;

        [SerializeField]
        private bool m_ScaleWithSize = false;

        public int DamageValue
        {
            get
            {
                return (int)((m_ScaleWithSize ? transform.localScale.MagnitudeNormalize() : 1) * m_DamageValue);
            }
        }

        protected override void OnTriggerSpecific(Collider other)
        {
            base.OnTriggerSpecific(other);

            if (other.CompareTag(gameObject.tag))
            {
                return;
            }

            GameObjectDestroyByContact otherGameObjectDestroyByContact = other.GetComponent<GameObjectDestroyByContact>();
            IDamageAble gameObjectDamageAble = gameObject.GetComponent<IDamageAble>();

            if (gameObjectDamageAble != null)
            {
                if(otherGameObjectDestroyByContact != null)
                {
                    gameObjectDamageAble.TakeDamage(otherGameObjectDestroyByContact.DamageValue);
                }
                else
                {
                    gameObjectDamageAble.TakeDamage(1);
                }                
            }
            else
            {
                Destroy(gameObject);
            }

            IDamageAble otherGameObjectDamageAble = other.GetComponent<IDamageAble>();

            if (otherGameObjectDamageAble != null)
            {
                otherGameObjectDamageAble.TakeDamage(DamageValue);
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}