using UnityEngine;

namespace Bachelor.MyExtensions
{
    public static class Vector3Extensions
    {
        public static float MagnitudeNormalize(this Vector3 vector3)
        {
            return vector3.magnitude / Vector3.one.magnitude;
        }

        public static float SqrMagnitudeNormalize(this Vector3 vector3)
        {
            return vector3.sqrMagnitude / Vector3.one.sqrMagnitude;
        }
    }
}