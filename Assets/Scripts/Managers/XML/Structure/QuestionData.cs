using System.Collections.Generic;
using System.Runtime.Serialization;
using Bachelor.Utilities;

namespace Bachelor.SerializeData.PlanetQuestion
{
    [DataContract(Name = "SolarSystem", Namespace = "")]
    public class QuestionData : ObjectData
    {
        [DataMember(Name = "Planets")]
        private List<Planet> m_PlanetList;

        public List<Planet> PlanetList
        {
            get
            {
                return m_PlanetList;
            }
        }
    }

    [DataContract(Namespace = "")]
    public class Planet
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember(Name = "Questions")]
        private List<Question> m_QuestionList;

        public List<Question> QuestionList
        {
            get
            {
                return m_QuestionList;
            }
        }
    }

    [DataContract(Namespace = "")]
    public class Question
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember(Name = "Answers")]
        private List<Answer> m_AnswerList;

        public List<Answer> AnswerList
        {
            get
            {
                return m_AnswerList;
            }
        }
    }

    [DataContract(Namespace = "")]
    public class Answer
    {
        [DataMember]
        public string Value { get; private set; }

        [DataMember]
        public bool Correct { get; private set; }
    }
}