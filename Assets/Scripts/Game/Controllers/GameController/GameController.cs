using Bachelor.Game.Base;
using Bachelor.Game.Controllers.GM.Base;
using Bachelor.MyExtensions.Managers;
using Bachelor.Utilities.Unity;
using UnityEngine;

namespace Bachelor.Game.Controllers
{
    public class GameController : GameControllerBase, INotification
    {
        public const string UpdateScoreNotification = "GameController.UpdateScoreNotification";

        public const string UpdateEnergyNotification = "GameController.UpdateEnergyNotification";

        [SerializeField]
        [Tooltip("Time in seconds")]
        private int m_MaxPlayingTime = 300;

        public int MaxPlayingTime
        {
            get
            {
                return m_MaxPlayingTime;
            }
        }

        [SerializeField]
        [ReadOnlyWhenPlaying]
        private int m_PlayingTimeElapsed;

        public int PlayingTimeElapsed
        {
            get
            {
                return m_PlayingTimeElapsed;
            }

            protected set
            {
                m_PlayingTimeElapsed = m_MaxPlayingTime - value;
                if(m_PlayingTimeElapsed < 0)
                {
                    m_PlayingTimeElapsed = 0;
                }
            }
        }

        [SerializeField]
        [ReadOnlyWhenPlaying]
        private float m_Score = 0f;

        public float Score
        {
            get
            {
                return m_Score;
            }
        }

        [SerializeField]
        [ReadOnlyWhenPlaying]
        private float m_Energy = 0f;

        public float Energy
        {
            get
            {
                return m_Energy;
            }

            protected set
            {                
                m_Energy = value;
                if(m_Energy < 0f)
                {
                    m_Energy = 0f;
                }
                else if(m_Energy > 1f)
                {
                    m_Energy = 1f;
                    
                }
                this.PostNotification(UpdateEnergyNotification);
            }
        }

        private bool m_GamePlaying = false;

        protected override void OnEnable()
        {
            base.OnEnable();
            this.AddObserver(OnUpdateScore, HealthBase.ObjectDeathNotification);
            this.AddObserver(OnUpdateEnergyIncrease, QuestionController.CorrectQuestionNotification);
            this.AddObserver(OnUpdateEnergyDecrease, QuestionController.IncorrectQuestionNotification);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            this.RemoveObserver(OnUpdateScore, HealthBase.ObjectDeathNotification);
            this.RemoveObserver(OnUpdateEnergyIncrease, QuestionController.CorrectQuestionNotification);
            this.RemoveObserver(OnUpdateEnergyDecrease, QuestionController.IncorrectQuestionNotification);
        }

        protected override void Start()
        {
            base.Start();
            ChangeState<StartingState>();
            UpdateGameTime();
            m_GamePlaying = true;
        }

        protected override void Update()
        {
            base.Update();
            if (m_GamePlaying)
            {
                UpdateGameTime();

                if (PlayingTimeElapsed <= 0f)
                {
                    this.PostNotification(PlayingState.GameOverNotification);
                    StopGame();

                }

                if (Energy >= 1f)
                {
                    this.PostNotification(PlayingState.GameCompleteNotification);
                    StopGame();
                }
            }
        }

        private void OnUpdateScore(object sender, object target)
        {
            if (sender is GameObjectHealth)
            {
                GameObjectHealth gameObjectHealth = sender as GameObjectHealth;
                if (gameObjectHealth.CompareTag("Enemy"))
                {
                    m_Score += gameObjectHealth.GetComponent<CreditBase>().Score;
                    this.PostNotification(UpdateScoreNotification);
                }                
            }
        }

        private void OnUpdateEnergyIncrease(object sender, object target)
        {
            if(sender is QuestionController)
            {
                Energy += (float)QuestionController.Instance.QuestionAmountCorrect / 100;
            }
        }

        private void OnUpdateEnergyDecrease(object sender, object target)
        {
            if (sender is QuestionController)
            {
                Energy -= (float)QuestionController.Instance.QuestionAmountIncorrect / 100;
            }
        }

        private void UpdateGameTime()
        {
            PlayingTimeElapsed = (int)Time.timeSinceLevelLoad;
        }

        private void StopGame()
        {
            m_GamePlaying = false;
            ChangeState<EndingState>();
        }
    }
}