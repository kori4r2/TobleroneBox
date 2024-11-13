using UnityEngine;
using UnityEngine.InputSystem;

namespace Toblerone.Toolbox {
    public class InputActivator : MonoBehaviour {
        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private BoolVariable inputEnabled;
        private VariableObserver<bool> inputEnabledObserver = null;

        private void Awake() {
            inputEnabledObserver = new VariableObserver<bool>(inputEnabled, UpdateActionsStatus);
        }

        private void UpdateActionsStatus(bool newStatus) {
            if (newStatus)
                EnableIfNecessary();
            else
                DisableIfNecessary();
        }

        private void EnableIfNecessary() {
            if (!inputActions.enabled)
                inputActions.Enable();
        }

        private void DisableIfNecessary() {
            if (inputActions.enabled)
                inputActions.Disable();
        }

        private void OnEnable() {
            EnableIfNecessary();
            inputEnabledObserver.StartWatching();
        }

        private void OnDisable() {
            inputEnabledObserver.StopWatching();
            DisableIfNecessary();
        }
    }
}
