using Bachelor.Utilities;
using System;
using UnityEngine;

namespace Bachelor.SerializeData
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameObjectPoolData", menuName = "GameObject/GameObject Pool data", order = 1)]
    public class GameObjectPoolDataList : SOBaseList<GameObjectPoolDataDetails>
    {
        [SerializeField]
        private string m_TransformView;

        public string TransformView
        {
            get
            {
                return m_TransformView;
            }
        }
    }
}