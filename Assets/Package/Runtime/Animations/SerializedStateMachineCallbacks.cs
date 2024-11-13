using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    public class SerializedStateMachineCallbacks : StateMachineBehaviour {
        [SerializeField] private UnityEvent StateEnterCallback;
        [SerializeField] private UnityEvent StateExitCallback;
        [SerializeField] private UnityEvent StateUpdateCallback;
        [SerializeField] private UnityEvent StateMoveCallback;
        [SerializeField] private UnityEvent StateIKCallback;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            StateEnterCallback?.Invoke();
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            StateExitCallback?.Invoke();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            StateUpdateCallback?.Invoke();
        }

        override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            StateMoveCallback?.Invoke();
        }

        override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            StateIKCallback?.Invoke();
        }
    }
}
