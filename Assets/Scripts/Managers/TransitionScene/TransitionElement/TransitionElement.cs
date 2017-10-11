using Bachelor.Managers.Transition.Base;
using UnityEngine;

namespace Bachelor.Managers.Transition
{
    public class TransitionElement : TransitionElementBase
    {
        [SerializeField]
        private TransitionDetails m_TransitionDetails;

        public override ITransitionDetails TransitionDetails
        {
            get
            {
                return m_TransitionDetails;
            }
        }
    }
}