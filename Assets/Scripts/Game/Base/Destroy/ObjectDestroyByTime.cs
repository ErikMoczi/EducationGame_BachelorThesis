using UnityEngine;

namespace Bachelor.Game.Base
{
    public class ObjectDestroyByTime : MonoBehaviour
    {
        [SerializeField]
        private float lifeTime = 1.5f;

        protected virtual void Start()
        {
            Destroy(gameObject, lifeTime);
        }
    }
}