using UnityEngine;

namespace Toblerone.Toolbox.SceneChangerSample {
    public class TransitionAnimationTriggers : MonoBehaviour {
        [SerializeField] private BoolEventSO animationStateChanged = null;
        private BoolEventListener stateChangeListener = null;
        [SerializeField] private Animator animator;
        [SerializeField] private string activationTrigger = "";
        [SerializeField] private string deactivationTrigger = "";

        private void Awake() {
            stateChangeListener ??= new BoolEventListener(animationStateChanged, ToggleAnimation);
            stateChangeListener.StartListeningEvent();
        }

        private void ToggleAnimation(bool value) {
            gameObject.SetActive(true);
            if (value)
                animator.SetTrigger(activationTrigger);
            else
                animator.SetTrigger(deactivationTrigger);
        }

        private void OnDestroy() {
            stateChangeListener.StopListeningEvent();
        }
    }
}
