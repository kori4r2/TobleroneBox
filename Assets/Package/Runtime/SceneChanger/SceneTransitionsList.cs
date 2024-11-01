using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox.SceneManagement {
    public class SceneTransitionsList {
        private HashSet<SceneTransitionInfo> activatedTransitionScenes = new HashSet<SceneTransitionInfo>();

        public void ActivateSceneTransition(SceneLoader sceneLoader, UnityAction<SceneLoader> SceneChangeCallback) {
            if (activatedTransitionScenes.Contains(sceneLoader.SceneTransitionInfo)) {
                SceneChangeCallback.Invoke(sceneLoader);
                return;
            }

            AsyncOperation loadOperation = sceneLoader.SceneTransitionInfo.LoadSceneAsync();
            loadOperation.completed += _ => SceneChangeCallback.Invoke(sceneLoader);
            activatedTransitionScenes.Add(sceneLoader.SceneTransitionInfo);
        }
    }
}
