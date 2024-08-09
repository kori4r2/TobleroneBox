using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toblerone.Toolbox.SceneManagement {
    [CreateAssetMenu(menuName = "TobleroneBox/SceneChanger/SceneTransitionInfo")]
    public class SceneTransitionInfo : ScriptableObject {
        [SerializeField] private ScenePicker transitionScene;
        [SerializeField] private SceneChangeControllerVariable sceneChangeController;
        public SceneChangeControllerVariable SceneChangeController => sceneChangeController;
        private Scene? loadedScene = null;
        public Scene LoadedScene {
            get {
                if (loadedScene == null) {
                    loadedScene = SceneManager.GetSceneByPath(transitionScene.Path);
                }
                return loadedScene.Value;
            }
        }

        public AsyncOperation LoadSceneAsync() {
            return SceneManager.LoadSceneAsync(transitionScene.Path, LoadSceneMode.Additive);
        }
    }
}
