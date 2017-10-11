using UnityEngine;
using System.Collections;
using Bachelor.Utilities;
using System.Collections.Generic;
using Handler = System.Action<System.Object, System.Object>;
using Bachelor.Managers.Transition;
using Bachelor.Managers.Transition.Base;
using Bachelor.MyExtensions.Managers;
using Bachelor.MyExtensions;

namespace Bachelor.Managers
{
    [RequireComponent(typeof(TransitionScene))]
    public partial class TransitionSceneManager : SingletonMonoBehaviourPersistent<TransitionSceneManager>, INotification
    {
        private const float m_AlphaMin = 0f;
        private const float m_AlphaMax = 1f;

        private const string TransitionSceneNotification = "TransitionSceneNotification";

        private Dictionary<ITransitionElement, ITransitionStatusControl> m_TransitionTable = new Dictionary<ITransitionElement, ITransitionStatusControl>();

        private TransitionScene m_TransitionScene;

        public int TransitionCount
        {
            get
            {
                return m_TransitionTable.Count;
            }
        }

        private TransitionSceneManager() { }

        protected override void Awake()
        {
            base.Awake();
            m_TransitionScene = GetComponent<TransitionScene>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopAllCoroutines();
        }

        public void QuitApplication()
        {
            m_TransitionScene.QuitApplication();
        }

        public void RestartApplication()
        {
            m_TransitionScene.RestartApplication();
        }

        public void RestartCurrentScene()
        {
            m_TransitionScene.RestartCurrentScene();
        }

        public void LoadNewScene(int sceneIndex)
        {
            m_TransitionScene.LoadNewScene(sceneIndex);
        }

        public void LoadNewScene(int sceneIndex, Handler handler)
        {
            m_TransitionScene.LoadNewScene(sceneIndex, handler);
        }

        public void LoadNewScene(int sceneIndex, bool animate, Handler handler = null)
        {
            m_TransitionScene.LoadNewScene(sceneIndex, animate, handler);
        }

        public void LoadNewScene(string sceneName)
        {
            m_TransitionScene.LoadNewScene(sceneName);
        }

        public void LoadNewScene(string sceneName, Handler handler)
        {
            m_TransitionScene.LoadNewScene(sceneName, handler);
        }

        public void LoadNewScene(string sceneName, bool animate, Handler handler = null)
        {
            m_TransitionScene.LoadNewScene(sceneName, animate, handler);
        }

        public void PlayTransition(ITransitionElement transitionElement, Handler handler = null)
        {
            if (!m_TransitionTable.ContainsKey(transitionElement))
            {
                m_TransitionTable.Add(transitionElement, new TransitionStatusControl());
            }
            else
            {
                if (GetTransitionStatus(transitionElement).InTransition)
                {
                    Debug.LogWarning(
                        "[TransitionScene] Element '" + transitionElement.Name +
                        "' already is in transition. Wait for it to finished."
                    );
                    return;
                }
            }

            if (handler != null)
            {
                this.AddObserver(handler, TransitionElementNotificationName(transitionElement));
            }

            ITransitionStatusControl transitionStatusControl = GetTransitionStatusControl(transitionElement);
            if (transitionStatusControl != null)
            {
                transitionStatusControl.Coroutine = SetUpTransition(transitionElement);
                StartCoroutine(transitionStatusControl.Coroutine);
            }
            ITransitionDetails transitionDetails = GetTransitionDetails(transitionElement);
            if (transitionDetails.TransitionElementChild != null)
            {
                PlayTransition(transitionDetails.TransitionElementChild);
            }
        }

        public void StopTransition(ITransitionElement transitionElement, bool visible = false)
        {
            ITransitionStatusControl transitionStatusControl = GetTransitionStatusControl(transitionElement);
            if (transitionStatusControl != null)
            {
                StopCoroutine(transitionStatusControl.Coroutine);
                EndTransition(transitionElement, visible);
            }
        }

        public ITransitionStatus GetTransitionStatus(ITransitionElement transitionElement)
        {
            if (!m_TransitionTable.ContainsKey(transitionElement))
            {
                return null;
            }
            return m_TransitionTable[transitionElement];
        }

        private IEnumerator SetUpTransition(ITransitionElement transitionElement)
        {
            StartTransition(transitionElement);
            {
                ITransitionDetails transitionDetails = transitionElement.TransitionDetails;
                do
                {
                    GetTransitionStatusControl(transitionElement).TransitionPercentBreak = 0f;
                    switch (transitionDetails.TransitionStyle)
                    {
                        case TransitionStyle.In:
                            {
                                yield return StartCoroutine(PLayFadeAnimation(transitionElement, true));
                                break;
                            }
                        case TransitionStyle.Out:
                            {
                                yield return StartCoroutine(PLayFadeAnimation(transitionElement, false));
                                break;
                            }
                        case TransitionStyle.InOut:
                            {
                                yield return StartCoroutine(PLayFadeAnimation(transitionElement, true));
                                GetTransitionStatusControl(transitionElement).TransitionPercentBreak = 0.5f;
                                yield return StartCoroutine(PLayFadeAnimation(transitionElement, false));
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                } while (transitionDetails.TransitionLoop);
            }
            EndTransition(transitionElement);
        }

        private void StartTransition(ITransitionElement transitionElement)
        {
            GetTransitionStatusControl(transitionElement).InTransition = true;
            SetTransitionCanvasEnable(GetTransitionDetails(transitionElement).TransitionOverlay, true);
        }

        private void EndTransition(ITransitionElement transitionElement, bool visible = false)
        {
            ITransitionDetails transitionDetails = GetTransitionDetails(transitionElement);
            if (transitionDetails.TransitionElementChild != null)
            {
                StopTransition(transitionDetails.TransitionElementChild);
            }

            if (!visible && !transitionDetails.VisibleAfterTransition)
            {
                SetTransitionCanvasEnable(transitionDetails.TransitionOverlay, false);
            }
            GetTransitionStatusControl(transitionElement).InTransition = false;
            this.PostNotification(TransitionElementNotificationName(transitionElement), transitionElement);
            m_TransitionTable.Remove(transitionElement);
        }

        private void CleanTransitionTable()
        {
            ITransitionElement[] transitionTableKeys = new ITransitionElement[m_TransitionTable.Keys.Count];
            m_TransitionTable.Keys.CopyTo(transitionTableKeys, 0);

            foreach (ITransitionElement transitionElement in transitionTableKeys)
            {
                ITransitionStatusControl transitionStatusControl = m_TransitionTable[transitionElement];
                if (!transitionStatusControl.InTransition)
                {
                    m_TransitionTable.Remove(transitionElement);
                }
            }
        }

        private IEnumerator PLayFadeAnimation(ITransitionElement transitionElement, bool forward)
        {
            if (forward)
            {
                GetTransitionStatusControl(transitionElement).TransitionFading = false;
                yield return StartCoroutine(PLayFadeAnimation(transitionElement, m_AlphaMin, m_AlphaMax));
            }
            else
            {
                GetTransitionStatusControl(transitionElement).TransitionFading = true;
                yield return StartCoroutine(PLayFadeAnimation(transitionElement, m_AlphaMax, m_AlphaMin));
            }
        }

        private IEnumerator PLayFadeAnimation(ITransitionElement transitionElement, float alphaStart, float alphaEnd)
        {
            ITransitionStatusControl transitionStatusControl = GetTransitionStatusControl(transitionElement);
            if (transitionStatusControl != null)
            {
                ITransitionDetails transitionDetails = GetTransitionDetails(transitionElement);
                float lerpValue = 0f;
                while (lerpValue <= 1f && transitionStatusControl.InTransition)
                {
                    lerpValue += Time.deltaTime / transitionDetails.TransitionDuration;
                    CalculateTransitionPercent(transitionElement, lerpValue);
                    transitionDetails.TransitionOverlay.alpha = Mathf.Lerp(alphaStart, alphaEnd, transitionDetails.Interpolation.Evaluate(lerpValue));
                    yield return null;
                }
            }
        }

        private void CalculateTransitionPercent(ITransitionElement transitionElement, float percent)
        {
            ITransitionStatusControl transitionStatusControl = GetTransitionStatusControl(transitionElement);
            ITransitionDetails transitionDetails = GetTransitionDetails(transitionElement);
            if (transitionDetails.TransitionStyle.Contains(TransitionStyle.InOut))
            {
                transitionStatusControl.TransitionPercent = percent / 2 + transitionStatusControl.TransitionPercentBreak;
            }
            else
            {
                transitionStatusControl.TransitionPercent = percent;
            }
        }

        private void SetTransitionCanvasEnable(CanvasGroup transitionOverlay, bool enabled)
        {
            transitionOverlay.gameObject.SetActive(enabled);
        }

        private string TransitionElementNotificationName(ITransitionElement transitionElement)
        {
            return TransitionSceneNotification + transitionElement.GetHashCode().ToString();
        }

        private ITransitionStatusControl GetTransitionStatusControl(ITransitionElement transitionElement)
        {
            return GetTransitionStatus(transitionElement) as ITransitionStatusControl;
        }

        private ITransitionDetails GetTransitionDetails(ITransitionElement transitionElement)
        {
            return transitionElement.TransitionDetails;
        }
    }
}