using UnityEngine;
using UnityEngine.InputSystem;

namespace Toblerone.Toolbox {
    public class InputActivator : MonoBehaviour {
        [SerializeField] private InputActionAsset inputActions;
        private void OnEnable() {
            inputActions.Enable();
        }

        private void OnDisable() {
            inputActions.Disable();
        }
    }
}
