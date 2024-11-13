using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Toblerone.Toolbox.SceneManagement {
    public class BasicSceneChangeController : SceneChangeController {
        private bool isActive = false;
        private bool showProgress = false;
        private AsyncOperation currentLoadOperation = null;
        [SerializeField] private SceneChangeControllerVariable reference = null;
        [SerializeField] private Image fillImage = null;
        [SerializeField] private GameObject rootObject = null;
        [Header("Animation Events")]
        [SerializeField] private BoolEventSO toggleTransitionAnimation = null;
        [SerializeField] private EventSO transitionAnimationFinished = null;
        private EventListener transitionAnimationListener = null;
        private UnityAction transitionCallback = null;

        private void Awake() {
            ResetParameters();
            reference.Value = this;
            if (transitionAnimationFinished == null || toggleTransitionAnimation == null)
                return;
            transitionAnimationListener = new EventListener(transitionAnimationFinished, OnAnimationFinished);
        }

        private void OnDestroy() {
            if (reference.Value == this)
                reference.Value = null;
            if (transitionAnimationListener != null && transitionAnimationFinished != null)
                transitionAnimationListener.StopListeningEvent();
        }

        private void ResetParameters() {
            isActive = false;
            showProgress = false;
            if (fillImage)
                fillImage.fillAmount = 0;
            currentLoadOperation = null;
        }

        private void Update() {
            if (!isActive || currentLoadOperation == null)
                return;

            if (fillImage)
                fillImage.fillAmount = showProgress ? currentLoadOperation.progress / 0.9f : 0;
        }

        private void OnAnimationFinished() {
            transitionAnimationListener.StopListeningEvent();
            UnityAction callback = transitionCallback;
            transitionCallback = null;
            if (isActive) {
                DeactivateImmediate(callback);
            } else {
                ActivateImmediate(callback);
            }
        }

        public override void Activate(UnityAction onPrepared) {
            if (transitionAnimationListener == null) {
                ActivateImmediate(onPrepared);
                return;
            }
            if (transitionCallback != null) {
                Debug.Log("[BasicSceneChangeController]: Tried to start animation transition while another is underway");
                return;
            }
            transitionCallback = onPrepared;
            transitionAnimationListener.StartListeningEvent();
            toggleTransitionAnimation.Raise(true);
            return;
        }

        private void ActivateImmediate(UnityAction onPrepared) {
            ResetParameters();
            rootObject.SetActive(true);
            onPrepared?.Invoke();
        }

        public override void Deactivate(UnityAction onFinish) {
            if (transitionAnimationListener == null) {
                DeactivateImmediate(onFinish);
                return;
            }
            if (transitionCallback != null) {
                Debug.Log("[BasicSceneChangeController]: Tried to start animation transition while another is underway");
                return;
            }
            transitionCallback = onFinish;
            transitionAnimationListener.StartListeningEvent();
            toggleTransitionAnimation.Raise(false);
            return;
        }

        private void DeactivateImmediate(UnityAction onFinish) {
            ResetParameters();
            rootObject.SetActive(false);
            onFinish?.Invoke();
        }

        public override void ManageSceneLoadOperation(AsyncOperation loadOperation) {
            if (isActive) {
                Debug.LogWarning("[BasicSceneChangeController]: Tried to manage a new load operation while already active");
                return;
            }
            currentLoadOperation = loadOperation;
            isActive = true;
            showProgress = true;
        }
    }
}
