using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Toblerone.Toolbox.SceneManagement {
    public static class SceneSwitcher {
        private static Dictionary<string, SceneLoader> ScenesLoaded { get; set; } = null;

        private static void CreateScenesLoadedDictionary() {
            ScenesLoaded = new Dictionary<string, SceneLoader>();
            for (int index = 0; index < SceneManager.sceneCount; index++) {
                Scene scene = SceneManager.GetSceneAt(index);
                ScenesLoaded.Add(scene.path, null);
            }
        }

        private static string currentMainScene = null;

        private static void InitVariables() {
            if (ScenesLoaded != null && currentMainScene != null)
                return;
            CreateScenesLoadedDictionary();
            currentMainScene = SceneManager.GetActiveScene().path;
        }

        private static SceneTransitionsList sceneTransitions = new SceneTransitionsList();
        private static bool isChangingScene = false;
        public static bool IsChangingScene {
            get => isChangingScene;
            private set {
                bool hasChanges = value != isChangingScene;
                isChangingScene = value;
                if (!hasChanges)
                    return;
                isChangingSceneUpdated.Invoke(isChangingScene);
            }
        }

        private static UnityEvent<bool> isChangingSceneUpdated = new UnityEvent<bool>();

        public static void ObserveSceneChangeStatus(UnityAction<bool> onStatusChanged) {
            if (onStatusChanged == null) {
                Debug.LogWarning($"[SceneSwitcher] onStatusChanged should not be null");
                return;
            }
            isChangingSceneUpdated.AddListener(onStatusChanged);
        }

        public static void StopObservingSceneChangeStatus(UnityAction<bool> onStatusChanged) {
            isChangingSceneUpdated.RemoveListener(onStatusChanged);
        }

        public static void LoadMainScene(SceneLoader sceneLoader) {
            if (IsChangingScene) {
                Debug.LogError($"[SceneSwitcher] Tried to switch scene to {sceneLoader.ScenePath} before another operation finished");
                return;
            }
            InitVariables();
            IsChangingScene = true;
            sceneTransitions.ActivateSceneTransition(sceneLoader, SwitchToNewMainScene);
        }

        private static void SwitchToNewMainScene(SceneLoader sceneLoader) {
            string previousMainScene = currentMainScene;
            SceneManager.SetActiveScene(sceneLoader.SceneTransitionInfo.LoadedScene);
            sceneLoader.SceneTransitionInfo.PrepareTransition(() => {
                AsyncOperation unloadOperation = UnloadExistingSceneAsync(previousMainScene);
                unloadOperation.completed += _ => LoadNewMainSceneAsync(sceneLoader);
            });
        }

        private static AsyncOperation UnloadExistingSceneAsync(string scenePath) {
            AsyncOperation unloadOperation;
            if (ScenesLoaded.ContainsKey(scenePath) && ScenesLoaded[scenePath] != null)
                unloadOperation = ScenesLoaded[scenePath].UnloadSceneAsync();
            else
                unloadOperation = SceneManager.UnloadSceneAsync(scenePath);
            ScenesLoaded.Remove(scenePath);
            return unloadOperation;
        }

        private static void LoadNewMainSceneAsync(SceneLoader sceneLoader) {
            AsyncOperation loadOperation = sceneLoader.LoadSceneAsync();
            loadOperation.completed += _ => UpdateNewMainSceneStatus(sceneLoader);
            sceneLoader.SceneTransitionInfo.ManageTransition(loadOperation);
        }

        private static void UpdateNewMainSceneStatus(SceneLoader newMainScene) {
            ScenesLoaded.Add(newMainScene.ScenePath, newMainScene);
            newMainScene.SceneTransitionInfo.EndTransition(() => {
                currentMainScene = newMainScene.ScenePath;
                SceneManager.SetActiveScene(newMainScene.LoadedScene);
                IsChangingScene = false;
            });
        }

        public static void LoadAdditionalScene(SceneLoader sceneLoader) {
            if (IsChangingScene) {
                Debug.LogError($"[SceneSwitcher] Tried to add scene {sceneLoader.ScenePath} before another operation finished");
                return;
            }
            InitVariables();
            if (ScenesLoaded.ContainsKey(sceneLoader.ScenePath)) {
                Debug.LogError($"[SceneSwitcher] Tried to load additive scene {sceneLoader.ScenePath} that is already loaded");
                return;
            }
            IsChangingScene = true;
            sceneTransitions.ActivateSceneTransition(sceneLoader, LoadNewSceneAdditive);
        }

        private static void LoadNewSceneAdditive(SceneLoader newAdditionalScene) {
            newAdditionalScene.SceneTransitionInfo.PrepareTransition(() => {
                AsyncOperation loadOperation = newAdditionalScene.LoadSceneAsync();
                loadOperation.completed += _ => FinishedAdditiveSceneLoad(newAdditionalScene);
                newAdditionalScene.SceneTransitionInfo.ManageTransition(loadOperation);
            });
        }

        private static void FinishedAdditiveSceneLoad(SceneLoader newAdditionalScene) {
            ScenesLoaded.Add(newAdditionalScene.ScenePath, newAdditionalScene);
            newAdditionalScene.SceneTransitionInfo.EndTransition(() => IsChangingScene = false);
        }

        public static void UnloadAdditionalScene(SceneLoader sceneLoader) {
            if (IsChangingScene) {
                Debug.LogError($"[SceneSwitcher] Tried to unload scene {sceneLoader.ScenePath} before another operation finished");
                return;
            }
            InitVariables();
            if (!ScenesLoaded.ContainsKey(sceneLoader.ScenePath)) {
                Debug.LogError($"[SceneSwitcher] Tried to unload scene {sceneLoader.ScenePath} that hasn't been loaded yet");
                return;
            }
            IsChangingScene = true;
            sceneTransitions.ActivateSceneTransition(sceneLoader, UnloadSceneAdditive);
        }

        private static void UnloadSceneAdditive(SceneLoader unloadedScene) {
            AsyncOperation unloadOperation = unloadedScene.UnloadSceneAsync();
            unloadOperation.completed += _ => FinishedAdditiveSceneUnload(unloadedScene);
            ScenesLoaded.Remove(unloadedScene.ScenePath);
            unloadedScene.SceneTransitionInfo.ManageTransition(unloadOperation);
        }

        private static void FinishedAdditiveSceneUnload(SceneLoader unloadedScene) {
            unloadedScene.SceneTransitionInfo.EndTransition(() => IsChangingScene = false);
        }
    }
}
