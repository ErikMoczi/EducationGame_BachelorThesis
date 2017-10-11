using System;
using UnityEngine;
using Bachelor.Utilities;

namespace Bachelor.SerializeData
{
    [Serializable]
    public class MusicPlaylistDetails : SODetails
    {
        [SerializeField]
        private AudioClip m_MusicFile;

        public AudioClip MusicFile
        {
            get
            {
                return m_MusicFile;
            }
        }
    }
}