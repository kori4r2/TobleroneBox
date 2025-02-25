using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Toblerone.Toolbox.SceneManagement {
    public class BasicSceneChangeController : SceneChangeController {
        protected bool isActive = false;
        protected AsyncOperation currentLoadOperation = null;
        [SerializeField] protected SceneChangeControllerVariable reference = null;
        [SerializeField] protected Image fillImage = null;
        [SerializeField] protected GameObject rootObject = null;
        [Header("Animation Events")]
        [SerializeField] protected BoolEventSO toggleTransitionAnimation = null;
        [SerializeField] protected EventSO transitionAnimationFinished = null;
        protected EventListener transitionAnimationListener = null;
        protected UnityAction transitionCallback = null;

        protected virtual void Awake() {
            ResetParameters();
            reference.Value = this;
            if (transitionAnimationFinished == null || toggleTransitionAnimation == null)
                return;
            transitionAnimationListener = new EventListener(transitionAnimationFinished, OnAnimationFinished);
        }

        protected virtual void OnDestroy() {
            if (reference.Value == this)
                reference.Value = null;
            if (transitionAnimationListener != null && transitionAnimationFinished != null)
                transitionAnimationListener.StopListeningEvent();
        }

        protected virtual void ResetParameters() {
            isActive = false;
            if (fillImage)
                fillImage.fillAmount = 0;
            currentLoadOperation = null;
        }

        protected virtual void Update() {
            if (!isActive || currentLoadOperation == null)
                return;

            if (fillImage)
                fillImage.fillAmount = currentLoadOperation.progress / 0.9f;
        }

        protected void OnAnimationFinished() {
            transitionAnimationListener.StopListeningEvent();
            if (transitionCallback == null) {
                Debug.LogError("[BasicSceneChangeController]: Animation transition finished, but callback is null");
                return;
            }
            UnityAction callback = transitionCallback;
            transitionCallback = null;
            if (isActive) {
                DeactivateImmediate(callback);
            } else {
                ActivateImmediate(callback);
            }
        }

        public override void Activate(UnityAction onPrepared) {
            if (transitionAnimationListener == null)
                ActivateImmediate(onPrepared);
            else
                WaitForAnimationTransition(onPrepared, true);
        }

        protected void ActivateImmediate(UnityAction onPrepared) {
            ResetParameters();
            rootObject.SetActive(true);
            onPrepared?.Invoke();
        }

        protected void WaitForAnimationTransition(UnityAction callback, bool toggleValue) {
            if (transitionCallback != null) {
                Debug.LogWarning("[BasicSceneChangeController]: Tried to start animation transition while another is underway");
                return;
            }
            transitionCallback = callback;
            transitionAnimationListener.StartListeningEvent();
            toggleTransitionAnimation.Raise(toggleValue);
        }

        public override void Deactivate(UnityAction onFinish) {
            if (transitionAnimationListener == null)
                DeactivateImmediate(onFinish);
            else
                WaitForAnimationTransition(onFinish, false);
        }

        protected void DeactivateImmediate(UnityAction onFinish) {
            ResetParameters();
            rootObject.SetActive(false);
            onFinish?.Invoke();
        }

        public override void DisplaySceneLoadOperation(AsyncOperation loadOperation) {
            if (isActive) {
                Debug.LogWarning("[BasicSceneChangeController]: Tried to manage a new load operation while already active");
                return;
            }
            currentLoadOperation = loadOperation;
            isActive = true;
        }
    }
}
