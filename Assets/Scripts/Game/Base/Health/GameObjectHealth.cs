using Bachelor.MyExtensions;
using Bachelor.MyExtensions.Managers;
using UnityEngine;

namespace Bachelor.Game.Base
{
    [RequireComponent(typeof(AudioSource))]
    public class GameObjectHealth : HealthBase, IGameObjectPool
    {
        [SerializeField]
        private bool m_ScaleWithSize = false;

        [SerializeField]
        private AudioClip m_TakeDamageAudio;

        [SerializeField]
        private GameObject m_ExplosionObject;

        public override int CurrentHealth
        {
            get
            {
                return (int)((m_ScaleWithSize ? transform.localScale.MagnitudeNormalize() : 1) * base.CurrentHealth);
            }
        }

        public override int StartingHealth
        {
            get
            {
                return (int)((m_ScaleWithSize ? transform.localScale.MagnitudeNormalize() : 1) * base.StartingHealth);
            }
        }

        private AudioSource m_AudioSource;

        protected virtual void Awake()
        {
            m_AudioSource = GetComponent<AudioSource>();
        }

        protected override void OnTakeDamage()
        {
            base.OnTakeDamage();
            PlayAudioSource(m_TakeDamageAudio);
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            this.Spawn(this.FindPoolName(m_ExplosionObject), transform.position, transform.rotation, 2f);
        }

        private void PlayAudioSource(AudioClip audioClip)
        {
            if(audioClip != null)
            {
                m_AudioSource.clip = audioClip;
                m_AudioSource.Play();
            }            
        }
    }
}