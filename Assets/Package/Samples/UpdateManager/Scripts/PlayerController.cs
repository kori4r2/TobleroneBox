using UnityEngine;
using UnityEngine.InputSystem;

namespace Toblerone.Toolbox.UpdateManagerSample {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private Rigidbody thisRigidBody;
        [SerializeField] private SpinningObjectRuntimeSet spinningObjects;
        [SerializeField] private InputActionReference movementActions;
        [SerializeField] private float moveSpeed = 5f;
        private bool shouldUpdateVelocity = true;
        private Vector3 currentVelocity = Vector3.zero;

        private void OnEnable() {
            movementActions.action.started += ApplyInputVelocity;
            movementActions.action.performed += ApplyInputVelocity;
            movementActions.action.canceled += StopMovement;
        }

        public void ApplyInputVelocity(InputAction.CallbackContext context) {
            shouldUpdateVelocity = true;
            currentVelocity = moveSpeed * ConvertInput(context.ReadValue<Vector2>());
        }

        private static Vector3 ConvertInput(Vector2 input) {
            return new Vector3(input.x, 0, input.y);
        }

        public void StopMovement(InputAction.CallbackContext context) {
            shouldUpdateVelocity = true;
            currentVelocity = Vector3.zero;
        }

        private void OnDisable() {
            movementActions.action.started -= ApplyInputVelocity;
            movementActions.action.performed -= ApplyInputVelocity;
            movementActions.action.canceled -= StopMovement;
        }

        private void OnTriggerEnter(Collider other) {
            SpinningObject spinningObject = spinningObjects.GetActiveElement(other.gameObject);
            if (!spinningObject)
                return;
            spinningObject.StartSpinning();
        }

        private void OnTriggerExit(Collider other) {
            SpinningObject spinningObject = spinningObjects.GetActiveElement(other.gameObject);
            if (!spinningObject)
                return;
            spinningObject.StopSpinning();
        }

        private void FixedUpdate() {
            if (shouldUpdateVelocity)
                thisRigidBody.velocity = currentVelocity;
            shouldUpdateVelocity = false;
        }
    }
}
