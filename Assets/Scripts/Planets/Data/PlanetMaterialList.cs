using UnityEngine;
using System;
using Bachelor.Utilities;

namespace Bachelor.SerializeData
{
    [Serializable]
    [CreateAssetMenu(fileName = "PlanetMaterialList", menuName = "Planet/Planet material List", order = 1)]
    public class PlanetMaterialList : SOBaseList<PlanetMaterialDetails>
    {
    }
}