using UnityEngine;

namespace Bachelor.Game.Base
{
    public abstract class ObjectDestroyByContactBase : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Boundery")
            {
                return;
            }
            OnTriggerSpecific(other);
        }

        protected virtual void OnTriggerSpecific(Collider other)
        {

        }
    }
}