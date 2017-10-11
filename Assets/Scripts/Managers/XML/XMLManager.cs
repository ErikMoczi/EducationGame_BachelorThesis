using Bachelor.Managers.XML;
using Bachelor.Utilities;

namespace Bachelor.Managers
{
    public class XMLManager : SingletonMonoBehaviourPersistent<XMLManager>
    {
        private PlanetContainer m_PlanetContainer;

        public PlanetContainer PlanetContainer
        {
            get
            {
                return m_PlanetContainer;
            }
        }

        private QuestionContainer m_QuestionContainer;

        public QuestionContainer QuestionContainer
        {
            get
            {
                return m_QuestionContainer;
            }
        }

        private XMLManager() { }

        protected override void Awake()
        {
            base.Awake();
            m_PlanetContainer = new PlanetContainer("XML", "planetInfoData.xml");
            m_QuestionContainer = new QuestionContainer("XML", "questionData.xml");
        }
    }
}