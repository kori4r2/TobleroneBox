using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Toblerone.Toolbox.SceneManagement {
    public static class SceneSwitcher {
        private static Dictionary<string, SceneLoader> scenesLoaded = null;
        private static Dictionary<string, SceneLoader> ScenesLoaded {
            get {
                if (scenesLoaded == null)
                    CreateScenesLoadedDictionary();
                return scenesLoaded;
            }
        }

        private static void CreateScenesLoadedDictionary() {
            scenesLoaded = new Dictionary<string, SceneLoader>();
            for (int index = 0; index < SceneManager.sceneCount; index++) {
                Scene scene = SceneManager.GetSceneAt(index);
                scenesLoaded.Add(scene.path, null);
            }
        }

        private static string currentMainScene = null;
        private static string CurrentMainScene {
            get {
                if (currentMainScene == null)
                    currentMainScene = SceneManager.GetActiveScene().path;
                return currentMainScene;
            }
            set => currentMainScene = value;
        }

        private static HashSet<SceneTransitionInfo> activatedTransitionScenes = new HashSet<SceneTransitionInfo>();
        private static SceneTransitionsList sceneTransitions = new SceneTransitionsList();
        public static bool IsChangingScene { get; private set; } = false;

        public static void LoadMainScene(SceneLoader sceneLoader) {
            if (IsChangingScene) {
                Debug.LogError($"[SceneSwitcher] Tried to switch scene to {sceneLoader.ScenePath} before another operation finished");
                return;
            }
            IsChangingScene = true;
            sceneTransitions.ActivateSceneTransition(sceneLoader, SwitchToNewMainScene);
        }

        private static void SwitchToNewMainScene(AsyncOperation loadingSceneLoadOperation, SceneLoader sceneLoader) {
            AsyncOperation unloadOperation = UnloadExistingSceneAsync(CurrentMainScene);
            unloadOperation.completed += op => LoadNewMainSceneAsync(op, sceneLoader);
            sceneLoader.SceneChangeController.ManageSceneUnloadOperation(unloadOperation);
        }

        private static AsyncOperation UnloadExistingSceneAsync(string scenePath) {
            AsyncOperation unloadOperation;
            if (ScenesLoaded[scenePath] != null)
                unloadOperation = scenesLoaded[scenePath].UnloadSceneAsync();
            else
                unloadOperation = SceneManager.UnloadSceneAsync(scenePath);
            scenesLoaded.Remove(scenePath);
            return unloadOperation;
        }

        private static void LoadNewMainSceneAsync(AsyncOperation unloadOperation, SceneLoader sceneLoader) {
            AsyncOperation loadOperation = sceneLoader.LoadSceneAsync();
            loadOperation.completed += op => UpdateNewMainSceneStatus(op, sceneLoader);
            sceneLoader.SceneChangeController.ManageSceneLoadOperation(loadOperation);
        }

        private static void UpdateNewMainSceneStatus(AsyncOperation loadOperation, SceneLoader newMainScene) {
            CurrentMainScene = newMainScene.ScenePath;
            ScenesLoaded.Add(newMainScene.ScenePath, newMainScene);
            IsChangingScene = false;
        }

        public static void LoadAdditionalScene(SceneLoader sceneLoader) {
            if (IsChangingScene) {
                Debug.LogError($"[SceneSwitcher] Tried to add scene {sceneLoader.ScenePath} before another operation finished");
                return;
            }
            IsChangingScene = true;
            sceneTransitions.ActivateSceneTransition(sceneLoader, LoadNewSceneAdditive);
        }

        private static void LoadNewSceneAdditive(AsyncOperation loadingSceneLoadOperation, SceneLoader newAdditionalScene) {
            AsyncOperation loadOperation = newAdditionalScene.LoadSceneAsync();
            loadOperation.completed += op => FinishedAdditiveSceneLoad(op, newAdditionalScene);
            newAdditionalScene.SceneChangeController.ManageSceneLoadOperation(loadOperation);
        }

        private static void FinishedAdditiveSceneLoad(AsyncOperation loadOperation, SceneLoader newAdditionalScene) {
            ScenesLoaded.Add(newAdditionalScene.ScenePath, newAdditionalScene);
            IsChangingScene = false;
        }

        public static void UnloadAdditionalScene(SceneLoader sceneLoader) {
            if (IsChangingScene) {
                Debug.LogError($"[SceneSwitcher] Tried to unload scene {sceneLoader.ScenePath} before another operation finished");
                return;
            }
            if (!ScenesLoaded.ContainsKey(sceneLoader.ScenePath)) {
                Debug.LogError($"[SceneSwitcher] Tried to unload scene {sceneLoader.ScenePath} that hasn't been loaded yet");
                return;
            }
            IsChangingScene = true;
            sceneTransitions.ActivateSceneTransition(sceneLoader, UnloadSceneAdditive);
        }

        private static void UnloadSceneAdditive(AsyncOperation loadingSceneLoadOperation, SceneLoader unloadedScene) {
            AsyncOperation unloadOperation = unloadedScene.UnloadSceneAsync();
            unloadOperation.completed += _ => IsChangingScene = false;
            ScenesLoaded.Remove(unloadedScene.ScenePath);
            unloadedScene.SceneChangeController.ManageSceneUnloadOperation(unloadOperation);
        }
    }
}
