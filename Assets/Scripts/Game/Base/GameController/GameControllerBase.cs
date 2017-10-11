using Bachelor.MyExtensions.Managers;
using UnityEngine;

namespace Bachelor.Game.Base
{
    public abstract class GameControllerBase : StateMachine, INotification, ITransitionScene
    {
        public const string PauseNotification = "GameControllerBase.PauseNotification";
        public const string ResumeNotification = "GameControllerBase.ResumeNotification";

        public const string PauseGameNotification = "GameControllerBase.PauseGameNotification";
        public const string ResumeGameNotification = "GameControllerBase.ResumeGameNotification";

        private bool m_Paused = false;

        public bool Pause
        {
            get
            {
                return m_Paused;
            }

            set
            {
                m_Paused = value;

                if (m_Paused)
                {
                    this.PostNotification(PauseNotification);
                    m_CurrentTimeScale = Time.timeScale;
                    Time.timeScale = 0f;
                }
                else
                {
                    this.PostNotification(ResumeNotification);
                    Time.timeScale = m_CurrentTimeScale;
                }
            }
        }

        private float m_CurrentTimeScale = 1f;

        protected virtual void OnEnable()
        {
            this.AddObserver(OnPause, PauseGameNotification);
            this.AddObserver(OnResume, ResumeGameNotification);
        }

        protected virtual void OnDisable()
        {
            this.RemoveObserver(OnPause, PauseGameNotification);
            this.RemoveObserver(OnResume, ResumeGameNotification);
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!Pause)
                {
                    Pause = true;
                    Time.timeScale = 0;
                }
                else
                {
                    Pause = false;
                    Time.timeScale = 1;
                }
            }
        }

        public virtual void RestartCurrentLevel()
        {
            Pause = false;
            this.RestartCurrentScene();
        }

        public virtual void BackToMenu()
        {
            Pause = false;
            this.LoadNewScene(2);
        }

        public virtual void ExitGame()
        {
            Pause = false;
            this.QuitApplication();
        }

        private void OnResume(object arg1, object arg2)
        {
            Pause = false;
        }

        private void OnPause(object arg1, object arg2)
        {
            Pause = true;
        }
    }
}
