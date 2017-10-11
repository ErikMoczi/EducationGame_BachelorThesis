using Bachelor.MyExtensions.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bachelor.Managers.Transition.Base
{
    public abstract class TransitionElementBase : MonoBehaviour, ITransitionElement
    {
        [SerializeField]
        protected Text m_TextPercent;

        abstract public ITransitionDetails TransitionDetails { get; }

        public string Name
        {
            get
            {
                return gameObject.name;
            }
        }

        protected virtual void Start()
        {
            if (TransitionDetails.TransitionOnWake)
            {
                this.PlayTransition();
            }
        }

        protected virtual void Update()
        {
            if (this.InTransitionSafe() && m_TextPercent != null)
            {
                m_TextPercent.text = Mathf.RoundToInt(this.GetTransitionStatus().TransitionPercent).ToString() + " %";
            }
        }

        protected virtual void OnDestroy()
        {
            try
            {
                if (this.InTransitionSafe())
                {
                    this.StopTransition();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}