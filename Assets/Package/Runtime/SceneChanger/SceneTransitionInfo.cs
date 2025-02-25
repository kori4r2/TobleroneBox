using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Toblerone.Toolbox.SceneManagement {
    [CreateAssetMenu(menuName = "TobleroneBox/SceneChanger/SceneTransitionInfo")]
    public class SceneTransitionInfo : ScriptableObject {
        [SerializeField] private ScenePicker transitionScene;
        [SerializeField] private SceneChangeControllerVariable sceneChangeController;
        private VariableObserver<SceneChangeController> sceneChangeControlerObserver;
        private AsyncOperation pendingOperation = null;
        private Scene? loadedScene = null;
        public Scene LoadedScene {
            get {
                if (loadedScene == null) {
                    loadedScene = SceneManager.GetSceneByPath(transitionScene.Path);
                }
                return loadedScene.Value;
            }
        }
        private bool NoTransition => transitionScene.IsEmpty || sceneChangeController == null;

        private void Awake() {
            sceneChangeControlerObserver = new VariableObserver<SceneChangeController>(
                sceneChangeController,
                OnChangeControllerUpdated
            );
        }

        public void PrepareTransition(UnityAction onPrepared) {
            if (onPrepared == null) {
                Debug.LogError($"[SceneTransition]: Transition callback missing for PrepareTransition call");
                return;
            }
            if (NoTransition)
                onPrepared.Invoke();
            else
                sceneChangeController.Value.Activate(onPrepared);
        }

        public void ManageTransition(AsyncOperation operation) {
            if (NoTransition)
                return;
            if (pendingOperation != null) {
                Debug.LogError($"[SceneTransition]: Tried to start a transition while operation is already pending.");
                if (sceneChangeController.Value != null)
                    EndTransition(null);
                return;
            }
            if (sceneChangeController.Value != null) {
                sceneChangeController.Value.DisplaySceneLoadOperation(operation);
            } else {
                pendingOperation = operation;
                sceneChangeControlerObserver.StartWatching();
            }
        }

        private void OnChangeControllerUpdated(SceneChangeController newValue) {
            if (newValue == null)
                return;
            StartPendingOperation();
        }

        private void StartPendingOperation() {
            sceneChangeControlerObserver.StopWatching();
            sceneChangeController.Value.DisplaySceneLoadOperation(pendingOperation);
            pendingOperation = null;
        }

        public void EndTransition(UnityAction onFinish) {
            if (onFinish == null) {
                Debug.LogError($"[SceneTransition]: Transition callback missing for EndTransition call");
                return;
            }
            if (NoTransition)
                onFinish.Invoke();
            else
                sceneChangeController.Value.Deactivate(onFinish);
        }

        public AsyncOperation LoadSceneAsync() {
            return NoTransition ? null : SceneManager.LoadSceneAsync(transitionScene.Path, LoadSceneMode.Additive);
        }
    }
}
