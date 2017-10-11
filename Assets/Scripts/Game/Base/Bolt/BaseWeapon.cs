using UnityEngine;
using System.Collections.Generic;

namespace Bachelor.Game.Base
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_Shot;

        public GameObject Shot
        {
            get
            {
                return m_Shot;
            }
        }

        [SerializeField]
        private Transform[] m_ShotSpawns;

        public Transform[] ShotSpawns
        {
            get
            {
                return m_ShotSpawns;
            }
        }

        [SerializeField]
        private float m_FireRate = 0.5f;

        public float FireRate
        {
            get
            {
                return m_FireRate;
            }
        }

        [SerializeField]
        private float m_Delay = 0f;

        public float Delay
        {
            get
            {
                return m_Delay;
            }
            protected
            set
            {
                m_Delay = value;
            }
        }

        [SerializeField]
        private AudioClip m_WeaponAudio;

        public AudioClip WeaponAudio
        {
            get
            {
                return m_WeaponAudio;
            }
        }

        private AudioSource m_WeaponSound;
        /*
        private List<GameObject> m_SpawnShots = new List<GameObject>();

        protected List<GameObject> SpawnShots
        {
            get
            {
                return m_SpawnShots;
            }
        }
        */

        protected AudioSource WeaponSound
        {
            get
            {
                return m_WeaponSound;
            }
        }

        protected virtual void Awake()
        {
            m_WeaponSound = GetComponent<AudioSource>();
        }

        protected virtual void FixedUpdate()
        {
            Fire();
        }

        protected abstract void Fire();

        protected void PlayWeaponSound()
        {
            PlayAudioSource(m_WeaponAudio);
        }
        /*
        public void RemoveSpawnShot(GameObject gameObject)
        {
            m_SpawnShots.Remove(gameObject);
        }
        */
        private void PlayAudioSource(AudioClip audioClip)
        {
            if (audioClip != null)
            {
                m_WeaponSound.clip = audioClip;
                m_WeaponSound.Play();
            }
        }
    }
}