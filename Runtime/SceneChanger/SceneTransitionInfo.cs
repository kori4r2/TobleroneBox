using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toblerone.Toolbox.SceneManagement {
    [CreateAssetMenu(menuName = "TobleroneBox/SceneChanger/SceneTransitionInfo")]
    public class SceneTransitionInfo : ScriptableObject, IVariableObserver<SceneChangeController> {
        [SerializeField] private ScenePicker transitionScene;
        [SerializeField] private SceneChangeControllerVariable sceneChangeController;
        public SceneChangeControllerVariable SceneChangeController => sceneChangeController;
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

        public void StartTransition(AsyncOperation operation) {
            if (pendingOperation != null) {
                Debug.LogError($"[SceneTransition]: Tried to start a transition while operation is already pending.");
                return;
            }
            if (sceneChangeController.Value != null) {
                sceneChangeController.Value.Activate();
                sceneChangeController.Value.ManageSceneLoadOperation(operation);
            } else {
                pendingOperation = operation;
                sceneChangeController.AddObserver(this);
            }
        }

        public void OnValueChanged(SceneChangeController newValue) {
            if (newValue == null)
                return;
            StartPendingOperation();
        }

        public void EndTransition() {
            sceneChangeController.Value.Deactivate();
        }

        public AsyncOperation LoadSceneAsync() {
            return SceneManager.LoadSceneAsync(transitionScene.Path, LoadSceneMode.Additive);
        }

        private void StartPendingOperation() {
            sceneChangeController.RemoveObserver(this);
            sceneChangeController.Value.ManageSceneLoadOperation(pendingOperation);
            pendingOperation = null;
        }
    }
}
