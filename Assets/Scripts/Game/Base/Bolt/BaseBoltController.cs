using UnityEngine;
using Bachelor.Utilities;

namespace Bachelor.Game.Base
{
    public abstract class BaseBoltController<T> : MonoBehaviour where T : BaseWeapon
    {
        private T m_WeaponController;

        public T WeaponController
        {
            get
            {
                return m_WeaponController;
            }
        }

        private string m_SpawnByName;

        public string SpawnByName
        {
            get
            {
                return m_SpawnByName;
            }

            set
            {
                m_SpawnByName = value;
            }
        }

        protected virtual void Start()
        {
            m_WeaponController = Utility.FindComponentByObjectName<T>(m_SpawnByName);
            gameObject.name = gameObject.name + gameObject.GetInstanceID().ToString();
        }

        protected virtual void OnDestroy()
        {
            if(m_WeaponController != null)
            {
                //m_WeaponController.RemoveSpawnShot(gameObject);
            }            
        }

        protected bool IsWeaponControllerAlive()
        {
            return m_WeaponController != null;
        }
    }
}