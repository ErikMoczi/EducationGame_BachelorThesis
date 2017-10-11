using UnityEngine;
using Bachelor.MyExtensions.Managers;

namespace Bachelor.Controllers
{
    public class StartUpController : MonoBehaviour, ITransitionScene
    {
        [SerializeField]
        private StartUpFactory m_StartUpFactory;

        private void Awake()
        {
            m_StartUpFactory.Init();
        }

        private void Start()
        {
            this.LoadNewScene(1, false);
            Destroy(gameObject);
        }
    }
}