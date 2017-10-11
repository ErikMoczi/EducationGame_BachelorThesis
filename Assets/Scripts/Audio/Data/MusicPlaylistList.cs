using UnityEngine;
using System;
using Bachelor.Utilities;

namespace Bachelor.SerializeData
{
    [Serializable]
    [CreateAssetMenu(fileName = "MusicPlaylist", menuName = "Audio/Music Play List", order = 1)]
    public class MusicPlaylistList : SOBaseList<MusicPlaylistDetails>
    {
    }
}