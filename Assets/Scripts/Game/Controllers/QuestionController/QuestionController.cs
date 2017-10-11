using Bachelor.Game.Base;
using Bachelor.Managers;
using Bachelor.Managers.XML;
using Bachelor.MyExtensions.Managers;
using Bachelor.SerializeData.PlanetQuestion;
using Bachelor.Utilities;
using System.Diagnostics;
using UnityEngine;

namespace Bachelor.Game.Controllers
{
    public class QuestionController : SingletonMonoBehaviour<QuestionController>, INotification
    {
        public const string CorrectQuestionNotification = "QuestionController.CorrectQuestionNotification";
        public const string IncorrectQuestionNotification = "QuestionController.IncorrectQuestionNotification";

        public const string DisplayQuestionNotification = "QuestionController.DisplayQuestionNotification";

        [SerializeField]
        private float m_MaxQuestionTime = 10.0f;

        public float MaxQuestionTime
        {
            get
            {
                return m_MaxQuestionTime;
            }
        }

        private float m_QuestionTimeElapse;

        public float QuestionTimeElapse
        {
            get
            {
                return m_QuestionTimeElapse;
            }

            protected set
            {
                m_QuestionTimeElapse = MaxQuestionTime - value;
                if (m_QuestionTimeElapse < 0)
                {
                    m_QuestionTimeElapse = 0;
                }
            }
        }

        [SerializeField]
        private float m_BaseQuestionChance = 0.2f;

        [SerializeField]
        private int m_QuestionAmountCorrect = 10;

        public int QuestionAmountCorrect
        {
            get
            {
                return m_QuestionAmountCorrect;
            }
        }

        [SerializeField]
        private int m_QuestionAmountIncorrect = 5;

        public int QuestionAmountIncorrect
        {
            get
            {
                return m_QuestionAmountIncorrect;
            }
        }

        private Question m_CurrentQuestion;

        public Question CurrentQuestion
        {
            get
            {
                return m_CurrentQuestion;
            }
        }

        private QuestionContainer m_QuestionContainer;
        private bool m_QuestionRunning = false;
        private Stopwatch m_Stopwatch;
        private float m_TimeScaleIncrement = 0f;

        private QuestionController() { }

        private void OnEnable()
        {
            this.AddObserver(OnQuestionDisplay, HealthBase.ObjectDeathNotification);
            this.AddObserver(OnPauseAction, GameControllerBase.PauseNotification);
            this.AddObserver(OnResumeAction, GameControllerBase.ResumeNotification);
        }

        private void OnDisable()
        {
            this.RemoveObserver(OnQuestionDisplay, HealthBase.ObjectDeathNotification);
            this.RemoveObserver(OnPauseAction, GameControllerBase.PauseNotification);
            this.RemoveObserver(OnResumeAction, GameControllerBase.ResumeNotification);
        }

        private void Start()
        {
            m_QuestionRunning = false;
            m_Stopwatch = new Stopwatch();
            m_QuestionContainer = XMLManager.Instance.QuestionContainer;
            if (!string.IsNullOrEmpty(SharedObject.GetString("planetLevel")))
            {
                m_QuestionContainer.SetPlanet(int.Parse(SharedObject.GetString("planetLevel")));
            }
        }

        private void FixedUpdate()
        {
            if (m_QuestionRunning)
            {
                QuestionTimeElapse = m_Stopwatch.Elapsed.Seconds;

                if (m_QuestionRunning && QuestionTimeElapse == m_MaxQuestionTime)
                {
                    Time.timeScale = 0f;
                }

                if (QuestionTimeElapse / m_MaxQuestionTime >= 0.6f)
                {
                    m_TimeScaleIncrement = 0.05f;
                }
                else
                {
                    m_TimeScaleIncrement = (1f - Time.timeScale) / QuestionTimeElapse;
                }

                Time.timeScale = Mathf.Lerp(0f, 1f, m_TimeScaleIncrement);
                UnityEngine.Debug.Log(Time.timeScale);

                if (QuestionTimeElapse <= 0f)
                {
                    m_QuestionRunning = false;
                    m_Stopwatch.Stop();
                    Time.timeScale = 1f;
                    this.PostNotification(IncorrectQuestionNotification);
                }
            }
        }

        public void ClickAnswerButton(int answer)
        {
            m_QuestionRunning = false;
            Time.timeScale = 1f;

            if (answer == m_QuestionContainer.FindCorrectAnswer(m_CurrentQuestion))
            {
                this.PostNotification(CorrectQuestionNotification);
            }
            else
            {
                this.PostNotification(IncorrectQuestionNotification);
            }
        }

        private void OnQuestionDisplay(object sender, object target)
        {
            if(sender is HealthBase)
            {
                CreditBase creditBase = (sender as HealthBase).GetComponent<CreditBase>();
                if(creditBase != null)
                {                    
                    StartQuestionSequence(creditBase.QuestionChance);
                }
                else
                {                    
                    StartQuestionSequence(m_BaseQuestionChance);
                }
            }
        }

        private void StartQuestionSequence(float chance)
        {
            if (!m_QuestionRunning)
            {
                float random = Random.Range(0f, 1.0f);
                if (random <= chance)
                {
                    m_QuestionRunning = true;

                    if (m_Stopwatch.IsRunning)
                    {
                        m_Stopwatch.Reset();
                    }

                    m_Stopwatch.Start();
                    m_CurrentQuestion = m_QuestionContainer.GenerateQuestion();
                    this.PostNotification(DisplayQuestionNotification);
                }
            }            
        }

        private void OnResumeAction(object arg1, object arg2)
        {
            if (!m_Stopwatch.IsRunning && m_QuestionRunning)
            {
                m_Stopwatch.Start();
            }
        }

        private void OnPauseAction(object arg1, object arg2)
        {
            if (m_Stopwatch.IsRunning)
            {
                m_Stopwatch.Stop();
            }
        }
    }
}