using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toblerone.Toolbox.SceneManagement {
    [CreateAssetMenu(menuName = "TobleroneBox/SceneChanger/SceneLoader")]
    public class SceneLoader : ScriptableObject {
        [SerializeField] private SceneTransitionInfo sceneTransitionInfo;
        public SceneTransitionInfo SceneTransitionInfo => sceneTransitionInfo;
        [SerializeField] private ScenePicker unityScene;
        public string ScenePath => unityScene.Path;
        public Scene LoadedScene => SceneManager.GetSceneByPath(ScenePath);

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
            return SceneManager.LoadSceneAsync(ScenePath, LoadSceneMode.Additive);
        }

        public AsyncOperation UnloadSceneAsync() {
            return SceneManager.UnloadSceneAsync(ScenePath);
        }
    }
}
