using Bachelor.Managers.Pool.Base;
using UnityEngine;

namespace Bachelor.Game
{
    public class BounderyCollisionDestroy : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            PoolObject poolObject = other.GetComponent<PoolObject>();
            if(poolObject != null)
            {
                poolObject.Despawn();
            }
            else
            {
                Destroy(other.gameObject);
            }            
        }
    }
}