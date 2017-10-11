using Bachelor.SerializeData;
using UnityEngine;

namespace Bachelor.Managers.Music
{
    public class MusicPlaylist : MonoBehaviour
    {
        [SerializeField]
        private bool m_PlayOnWake = true;

        [SerializeField]
        private MusicPlaylistList m_MusicPlaylistList;

        private void Start()
        {
            if (m_PlayOnWake)
            {
                MusicManager.Instance.ChangePlaylist(m_MusicPlaylistList);
            }
        }
    }
}