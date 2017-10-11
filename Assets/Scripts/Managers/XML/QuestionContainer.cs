using Bachelor.MyExtensions;
using Bachelor.SerializeData.PlanetQuestion;
using System.Collections.Generic;

namespace Bachelor.Managers.XML
{
    public class QuestionContainer : XMLObjectContainer<QuestionData>
    {
        private Planet m_CurrentPlanet;
        public Planet CurrentPlanet
        {
            get
            {
                return m_CurrentPlanet;
            }
        }

        private int m_DefaultPlanet = 3;

        public QuestionContainer(string folderPath, string fileName, bool resourceFile = true) : base(folderPath, fileName, resourceFile)
        {
            SetPlanet(m_DefaultPlanet);
        }

        public void SetPlanet(int id)
        {
            m_CurrentPlanet = FindPlanet(id);
        }

        public Question GenerateQuestion()
        {
            return TakeRandomQuestion(CurrentPlanet);
        }

        public List<Answer> GetAnswers(Question question)
        {
            return question.AnswerList;
        }

        public string GetAnswer1(Question question)
        {
            return question.AnswerList[0].Value;
        }

        public string GetAnswer2(Question question)
        {
            return question.AnswerList[1].Value;
        }

        public string GetAnswer3(Question question)
        {
            return question.AnswerList[2].Value;
        }

        public int FindCorrectAnswer(Question question)
        {
            int correctAns = 0;
            foreach (Answer answer in question.AnswerList)
            {
                correctAns++;
                if (answer.Correct)
                {
                    break;
                }
            }
            return correctAns;
        }

        private Planet FindPlanet(int findId)
        {
            return m_ObjectData.PlanetList.Find(x => x.Id == findId);
        }

        private Question TakeRandomQuestion(Planet planet)
        {
            return planet.QuestionList.PickRandom();
        }
    }
}