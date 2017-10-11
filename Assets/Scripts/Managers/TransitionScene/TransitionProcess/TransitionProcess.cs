using System.Collections;
using UnityEngine;
using Handler = System.Action<System.Object, System.Object>;
using Bachelor.MyExtensions.Managers;

namespace Bachelor.Managers.Transition.Base
{
    public abstract partial class TransitionProcess : TransitionElementBase
    {
        [SerializeField]
        private TransitionDetailsBase m_TransitionDetailsBase;

        public override ITransitionDetails TransitionDetails
        {
            get
            {
                return m_TransitionDetailsExtended;
            }
        }

        private ITransitionDetailsExtended m_TransitionDetailsExtended;

        private bool m_InTransitionProcess = false;

        public bool InTransitionProcess
        {
            get
            {
                return m_InTransitionProcess;
            }
        }

        protected virtual void Awake()
        {
            InitTransitionDetails();
        }

        protected void PlayTransitionProcess(Handler middleHandler, Handler finishHandler = null)
        {
            if (!m_InTransitionProcess)
            {
                StartCoroutine(SetTransitionProcess(middleHandler, finishHandler));
            }
        }

        private IEnumerator SetTransitionProcess(Handler middleHandler, Handler finishHandler = null)
        {
            m_InTransitionProcess = true;
            SetUpTransitionDetailsIn();
            this.PlayTransition(middleHandler);
            while (this.InTransitionSafe())
            {
                yield return null;
            }
            SetUpTransitionDetailsOut();
            this.PlayTransition(finishHandler);
            while (this.InTransitionSafe())
            {
                yield return null;
            }
            m_InTransitionProcess = false;
        }

        private void InitTransitionDetails()
        {
            if (m_TransitionDetailsExtended == null)
            {
                m_TransitionDetailsExtended = new TransitionDetailsExtended(m_TransitionDetailsBase, false, TransitionStyle.None, false, false);
            }
            m_TransitionDetailsExtended.SetTransitionDuration(m_TransitionDetailsBase.TransitionDuration / 2);
        }

        private void SetUpTransitionDetailsIn()
        {
            m_TransitionDetailsExtended.SetTransitionStyle(TransitionStyle.In);
        }

        private void SetUpTransitionDetailsOut()
        {
            m_TransitionDetailsExtended.SetTransitionStyle(TransitionStyle.Out);
        }
    }
}