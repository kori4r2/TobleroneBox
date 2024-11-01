using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Toblerone.Toolbox.SceneManagement {
    [CreateAssetMenu(menuName = "TobleroneBox/SceneChanger/SceneTransitionInfo")]
    public class SceneTransitionInfo : ScriptableObject {
        [SerializeField] private ScenePicker transitionScene;
        [SerializeField] private SceneChangeControllerVariable sceneChangeController;
        public SceneChangeControllerVariable SceneChangeController => sceneChangeController;
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
            sceneChangeController.Value.Activate(onPrepared);
        }

        public void ManageTransition(AsyncOperation operation) {
            if (pendingOperation != null) {
                Debug.LogError($"[SceneTransition]: Tried to start a transition while operation is already pending.");
                if (sceneChangeController.Value != null)
                    EndTransition(null);
                return;
            }
            if (sceneChangeController.Value != null) {
                sceneChangeController.Value.ManageSceneLoadOperation(operation);
            } else {
                pendingOperation = operation;
                sceneChangeControlerObserver.StartWatching();
            }
        }

        public void OnChangeControllerUpdated(SceneChangeController newValue) {
            if (newValue == null)
                return;
            StartPendingOperation();
        }

        private void StartPendingOperation() {
            sceneChangeControlerObserver.StopWatching();
            sceneChangeController.Value.ManageSceneLoadOperation(pendingOperation);
            pendingOperation = null;
        }

        public void EndTransition(UnityAction onFinish) {
            sceneChangeController.Value.Deactivate(onFinish);
        }

        public AsyncOperation LoadSceneAsync() {
            return SceneManager.LoadSceneAsync(transitionScene.Path, LoadSceneMode.Additive);
        }
    }
}
