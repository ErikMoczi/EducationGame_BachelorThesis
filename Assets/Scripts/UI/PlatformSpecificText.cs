using UnityEngine;
using UnityEngine.UI;

namespace Bachelor.UI
{
    [RequireComponent(typeof(Text))]
    public class PlatformSpecificText : MonoBehaviour
    {
        [SerializeField]
        private string m_TextMobile;

        [SerializeField]
        private string m_TextStandalone;

        private void Awake()
        {
            Text text = GetComponent<Text>();

#if UNITY_STANDALONE || UNITY_STANDALONE_OSX || UNITY_EDITOR
            text.text = m_TextStandalone;
#else
        text.text = m_TextMobile;
#endif
        }
    }
}