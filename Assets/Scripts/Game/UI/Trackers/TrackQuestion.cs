using Bachelor.Game.Base;
using Bachelor.Game.Controllers;
using Bachelor.Managers;
using Bachelor.Managers.XML;
using Bachelor.MyExtensions.Managers;
using Bachelor.SerializeData.PlanetQuestion;
using UnityEngine;
using UnityEngine.UI;

namespace Bachelor.Game
{
    [RequireComponent(typeof(AudioSource))]
    public class TrackQuestion : TrackBase, INotification
    {
        [SerializeField]
        private Text m_QuestionText;

        [SerializeField]
        private Text m_AnswerButton1;

        [SerializeField]
        private Text m_AnswerButton2;

        [SerializeField]
        private Text m_AnswerButton3;

        [SerializeField]
        private AudioClip m_AudioClipCorrect;

        [SerializeField]
        private AudioClip m_AudioClipIncorrect;

        [SerializeField]
        private GameObject m_QuestionPanel;

        [SerializeField]
        private Image m_TimeIndicator;

        [SerializeField]
        private Text m_TimeNumber;

#if UNITY_EDITOR
        [SerializeField]
        private Image[] m_CheatText;
#endif        

        private AudioSource m_AudioSource;
        private bool m_QuestionDisplay = false;
        private QuestionController m_QuestionController;

        private void OnEnable()
        {
            this.AddObserver(OnQuestionDisplay, QuestionController.DisplayQuestionNotification);
            this.AddObserver(OnQuestionCorrect, QuestionController.CorrectQuestionNotification);
            this.AddObserver(OnQuestionIncorrect, QuestionController.IncorrectQuestionNotification);
        }

        private void OnDisable()
        {
            this.RemoveObserver(OnQuestionDisplay, QuestionController.DisplayQuestionNotification);
            this.RemoveObserver(OnQuestionCorrect, QuestionController.CorrectQuestionNotification);
            this.RemoveObserver(OnQuestionIncorrect, QuestionController.IncorrectQuestionNotification);
        }        

        private void Awake()
        {
            m_AudioSource = GetComponent<AudioSource>();
            m_QuestionController = QuestionController.Instance;
        }

        private void Start()
        {
            HideQuestionPanel();
        }

        private void Update()
        {
            if (m_QuestionDisplay)
            {
                m_TimeIndicator.fillAmount = m_QuestionController.QuestionTimeElapse / m_QuestionController.MaxQuestionTime;
                m_TimeNumber.text = ((int)m_QuestionController.QuestionTimeElapse).ToString();
            }            
        }

        private void OnQuestionDisplay(object arg1, object arg2)
        {
            ShowQuestionPanel();
            Question question = m_QuestionController.CurrentQuestion;
            QuestionContainer questionContainer = XMLManager.Instance.QuestionContainer;

            m_QuestionText.text = question.Name;
            m_AnswerButton1.text = questionContainer.GetAnswer1(question);
            m_AnswerButton2.text = questionContainer.GetAnswer2(question);
            m_AnswerButton3.text = questionContainer.GetAnswer3(question);

#if UNITY_EDITOR
            m_CheatText[questionContainer.FindCorrectAnswer(question) - 1].gameObject.SetActive(true);
#endif
        }

    private void OnQuestionCorrect(object arg1, object arg2)
        {
            HideQuestionPanel();
            PlayAudioSource(m_AudioClipCorrect);
        }

        private void OnQuestionIncorrect(object arg1, object arg2)
        {
            HideQuestionPanel();
            PlayAudioSource(m_AudioClipIncorrect);
        }

        private void HideQuestionPanel()
        {
            m_QuestionPanel.SetActive(false);
            m_QuestionDisplay = false;

#if UNITY_EDITOR
            foreach(var cheatText in m_CheatText)
            {
                cheatText.gameObject.SetActive(false);
            }
#endif
        }

        private void ShowQuestionPanel()
        {
            m_QuestionPanel.SetActive(true);
            m_QuestionDisplay = true;
        }

        private void PlayAudioSource(AudioClip audioClip)
        {
            if (audioClip != null)
            {
                m_AudioSource.clip = audioClip;
                m_AudioSource.Play();
            }
        }
    }
}