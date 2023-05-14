using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toblerone.Toolbox.SceneManagement {
    [CreateAssetMenu(menuName = "TobleroneBox/SceneChanger/SceneLoader")]
    public class SceneLoader : ScriptableObject {
        [SerializeField] private SceneTransitionInfo sceneTransitionInfo;
        public SceneTransitionInfo SceneTransitionInfo => sceneTransitionInfo;
        [SerializeField] private ScenePicker transitionScene;
        public string ScenePath => transitionScene.Path;
        private Scene? loadedScene = null;
        public Scene LoadedScene {
            get {
                if (loadedScene == null) {
                    loadedScene = SceneManager.GetSceneByPath(ScenePath);
                }
                return loadedScene.Value;
            }
        }
        public SceneChangeController SceneChangeController => sceneTransitionInfo.SceneChangeController.Value;

        public void LoadAsMainScene() {
            SceneSwitcher.LoadMainScene(this);
        }

        public void LoadAdditive() {
            SceneSwitcher.LoadAdditionalScene(this);
        }

        public void UnloadAdditive() {
            SceneSwitcher.UnloadAdditionalScene(this);
        }

        public AsyncOperation LoadSceneAsync() {
            return SceneManager.LoadSceneAsync(ScenePath);
        }

        public AsyncOperation UnloadSceneAsync() {
            return SceneManager.LoadSceneAsync(ScenePath);
        }
    }
}
