using UnityEngine;

namespace Bachelor.Game.Base
{
    public abstract class State : MonoBehaviour
    {
        public virtual void Begin()
        {
        }

        public virtual void End()
        {
            Destroy(this);
        }
    }
}