using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Toblerone.Toolbox {
    [System.Serializable]
    public class PointerInputProcessor {
        [SerializeField] private InputActionReference pointerPressAction;
        [SerializeField] private InputActionReference pointerPositionAction;
        private Vector2 pointerPosition = Vector2.zero;
        public Vector2 PointerPosition => pointerPosition;

        [SerializeField] private UnityEvent onPress = new UnityEvent();
        public UnityEvent OnPress => onPress;
        [SerializeField] private UnityEvent onRelease = new UnityEvent();
        public UnityEvent OnRelease => onRelease;
        private Camera mainCamera = null;
        public Camera MainCamera {
            get {
                mainCamera = mainCamera ? mainCamera : Camera.main;
                return mainCamera;
            }
        }

        public PointerInputProcessor(InputActionReference pressAction, InputActionReference positionAction) {
            pointerPressAction = pressAction;
            pointerPositionAction = positionAction;
        }

        public void Enable() {
            pointerPressAction.action.started += OnPressPointer;
            pointerPressAction.action.canceled += OnReleasePointer;
            pointerPositionAction.action.performed += OnMovePointer;
        }

        public void Disable() {
            pointerPressAction.action.started -= OnPressPointer;
            pointerPressAction.action.canceled -= OnReleasePointer;
            pointerPositionAction.action.performed -= OnMovePointer;
        }

        private void OnPressPointer(InputAction.CallbackContext context) {
            Vector2 currentPositionActionValue = pointerPositionAction.action.ReadValue<Vector2>();
            UpdatePointerPosition(currentPositionActionValue);
            onPress?.Invoke();
        }

        private void OnReleasePointer(InputAction.CallbackContext context) {
            onRelease?.Invoke();
        }

        private void OnMovePointer(InputAction.CallbackContext context) {
            Vector2 actionValue = context.ReadValue<Vector2>();
            UpdatePointerPosition(actionValue);
        }

        private void UpdatePointerPosition(Vector2 actionValue) {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Vector2 screenPosition = ClampVector2(actionValue, Vector2.zero, screenSize);
            pointerPosition = MainCamera.ScreenToWorldPoint(screenPosition);
        }

        private static Vector2 ClampVector2(Vector2 value, Vector2 minValue, Vector2 maxValue) {
            return new Vector2(Mathf.Clamp(value.x, minValue.x, maxValue.x), Mathf.Clamp(value.y, minValue.y, maxValue.y));
        }
    }
}
