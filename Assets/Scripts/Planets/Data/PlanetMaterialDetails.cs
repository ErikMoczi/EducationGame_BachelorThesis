using System;
using UnityEngine;
using Bachelor.Utilities;

namespace Bachelor.SerializeData
{
    [Serializable]
    public class PlanetMaterialDetails : SODetails
    {
        [SerializeField]
        private int m_Id;

        public int Id
        {
            get
            {
                return m_Id;
            }
        }

        [SerializeField]
        private Material m_Material;

        public Material Material
        {
            get
            {
                return m_Material;
            }
        }
    }
}