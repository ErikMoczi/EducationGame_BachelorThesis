using Bachelor.Managers;
using UnityEngine;

namespace Bachelor.Controllers
{
    public class StartUpFactory : MonoBehaviour
    {
        [SerializeField]
        private TransitionSceneManager m_TransferSceneManagerPrefab;

        [SerializeField]
        private NotificationCenter m_NotificationCenterPrefab;

        [SerializeField]
        private MusicManager m_MusicManagerPrefab;

        [SerializeField]
        private XMLManager m_XMLManager;

        public void Init()
        {
            Instantiate(m_TransferSceneManagerPrefab);
            Instantiate(m_NotificationCenterPrefab);
            Instantiate(m_MusicManagerPrefab);
            Instantiate(m_XMLManager);
        }
    }
}