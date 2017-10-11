using Bachelor.Managers.Music;
using Bachelor.SerializeData;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bachelor.Utilities;

namespace Bachelor.Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : SingletonMonoBehaviourPersistent<MusicManager>
    {
        private const float m_VolumeMin = 0f;
        private const float m_VolumeMax = 1f;

        private const float m_FadeDurationMin = 0f;
        private const float m_FadeDurationMax = 3f;

        [SerializeField, Range(m_VolumeMin, m_VolumeMax)]
        private float m_Volume = 0.5f;

        public float Volume
        {
            get
            {
                return m_AudioSource.volume;
            }

            set
            {
                m_AudioSource.volume = m_Volume = Mathf.Clamp(value, m_VolumeMin, m_VolumeMax);
            }
        }

        [SerializeField]
        private MusicPlaylistList m_MusicPlaylistList;

        public MusicPlaylistList MusicPlaylistList
        {
            get
            {
                return m_MusicPlaylistList;
            }
        }

        [SerializeField]
        private bool m_Shuffle = true;

        [SerializeField]
        private RepeatMode m_RepeatMode;

        [SerializeField, Range(m_FadeDurationMin, m_FadeDurationMax)]
        private float m_FadeDuration = 2f;

        public float FadeDuration
        {
            get
            {
                return m_FadeDuration;
            }

            set
            {
                m_FadeDuration = Mathf.Clamp(value, m_FadeDurationMin, m_FadeDurationMax);
            }
        }

        [SerializeField]
        private bool m_PlayOnAwake = true;

        private AudioSource m_AudioSource;

        public AudioSource AudioSource
        {
            get
            {
                return m_AudioSource;
            }
        }

        private MusicPlaylistList m_DefaultMusicPlaylistList;
        private int m_CurrentAudioClipIndex = 0;

        public AudioClip CurrentAudioClip
        {
            get
            {
                return m_MusicPlaylistList[m_CurrentAudioClipIndex].MusicFile;
            }
        }

        private bool m_StopShuffle = false;
        private bool m_AudioSourcePause = false;

        private MusicManager() { }

        protected override void Awake()
        {
            base.Awake();
            SetDefault();
        }

        private void Start()
        {
            if (m_PlayOnAwake)
            {
                Play();
            }
        }

        public void Play()
        {
            if (!IsPlaying() && m_MusicPlaylistList)
            {
                StartCoroutine(PlayMusicPlaylist());
            }
        }

        public bool IsPlaying()
        {
            return m_AudioSource.isPlaying || m_AudioSourcePause;
        }

        public void Stop(bool fade = false)
        {
            StopAllCoroutines();
            if (fade)
            {
                StartCoroutine(StopWithFade());
            }
            else
            {
                m_AudioSource.Stop();
            }
        }

        public void Next()
        {
            Stop();
            NextAudioClip();
            Play();
        }

        public void Pause()
        {
            if (m_AudioSourcePause)
            {
                m_AudioSource.UnPause();
                m_AudioSourcePause = false;
            }
            else
            {
                m_AudioSource.Pause();
                m_AudioSourcePause = true;
            }
        }

        public bool IsPaused()
        {
            return m_AudioSourcePause;
        }

        public void ChangePlaylist(MusicPlaylistList newMusicPlaylistList)
        {
            if (newMusicPlaylistList)
            {
                m_MusicPlaylistList = newMusicPlaylistList;
                HandleChangePlaylist();
                SceneManager.activeSceneChanged += OnSceneChanged;
            }
        }

        private void SetDefault()
        {
            m_AudioSource = GetComponent<AudioSource>();
            m_DefaultMusicPlaylistList = MusicPlaylistList;
            SetAudioSourceDefaultSettings();
        }

        private void SetAudioSourceDefaultSettings()
        {
            m_AudioSource.playOnAwake = false;
        }

        private void OnSceneChanged(Scene currentScene, Scene newScene)
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
            m_MusicPlaylistList = m_DefaultMusicPlaylistList;
            HandleChangePlaylist();
        }

        private void HandleChangePlaylist()
        {
            if (IsPlaying())
            {
                StartCoroutine(StopWithFade());
            }
            SetPlaylistToStart();
            Play();
        }

        private IEnumerator PlayMusicPlaylist()
        {
            bool endLess = true;
            while (endLess)
            {
                HandleShuffle();
                yield return StartCoroutine(PlayClip(m_MusicPlaylistList[m_CurrentAudioClipIndex].MusicFile));
                switch (m_RepeatMode)
                {
                    case RepeatMode.Track:
                        {
                            break;
                        }
                    case RepeatMode.Playlist:
                        {
                            if (!m_Shuffle)
                            {
                                NextAudioClip();
                            }
                            break;
                        }
                    default:
                        {
                            endLess = false;
                            break;
                        }
                }
            }
        }

        private IEnumerator StopWithFade()
        {
            if (m_FadeDuration > m_FadeDurationMin)
            {
                float lerpValue = m_VolumeMin;
                while (lerpValue < m_VolumeMax)
                {
                    lerpValue += Time.deltaTime / m_FadeDuration;
                    m_AudioSource.volume = Mathf.Lerp(m_Volume, m_VolumeMin, lerpValue);
                    yield return null;
                }
            }
            m_AudioSource.Stop();
        }

        private IEnumerator PlayClip(AudioClip audioClip)
        {
            m_AudioSource.clip = audioClip;
            m_AudioSource.Play();
            StartCoroutine(FadeIn());
            while (IsPlaying())
            {
                if (m_AudioSource.clip.length - m_AudioSource.time <= m_FadeDuration)
                {
                    yield return StartCoroutine(FadeOut());
                }
                yield return null;
            }
        }

        private IEnumerator FadeOut()
        {
            if (m_FadeDuration > m_FadeDurationMin)
            {
                float startTime = m_AudioSource.clip.length - m_FadeDuration;
                float lerpValue = m_VolumeMin;
                while (lerpValue < m_VolumeMax && IsPlaying())
                {
                    lerpValue = Mathf.InverseLerp(startTime, m_AudioSource.clip.length, m_AudioSource.time);
                    m_AudioSource.volume = Mathf.Lerp(m_Volume, m_VolumeMin, lerpValue);
                    yield return null;
                }
                m_AudioSource.volume = m_VolumeMin;
            }
            else
            {
                yield break;
            }
        }

        private IEnumerator FadeIn()
        {
            if (m_FadeDuration > m_FadeDurationMin)
            {
                float lerpValue = m_VolumeMin;
                while (lerpValue < m_VolumeMax && IsPlaying())
                {
                    lerpValue = Mathf.InverseLerp(m_VolumeMin, m_FadeDuration, m_AudioSource.time);
                    m_AudioSource.volume = Mathf.Lerp(m_VolumeMin, m_Volume, lerpValue);
                    yield return null;
                }
                m_AudioSource.volume = m_Volume;
            }
            else
            {
                yield break;
            }
        }

        private int GetNewTrack()
        {
            int newTrack = Random.Range(0, MusicPlaylistList.Count);
            while (newTrack == m_CurrentAudioClipIndex && m_MusicPlaylistList.Count > 1)
            {
                newTrack = GetNewTrack();
            }
            return newTrack;
        }

        private void HandleShuffle()
        {
            if (m_Shuffle && !m_StopShuffle)
            {
                m_CurrentAudioClipIndex = GetNewTrack();
            }
            m_StopShuffle = m_RepeatMode == RepeatMode.Track ? true : false;
        }

        private void NextAudioClip()
        {
            m_CurrentAudioClipIndex++;
            if (m_CurrentAudioClipIndex >= m_MusicPlaylistList.Count)
            {
                SetPlaylistToStart();
            }
        }

        private void SetPlaylistToStart()
        {
            m_CurrentAudioClipIndex = 0;
        }
    }
}