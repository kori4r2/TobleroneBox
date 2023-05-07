using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox.SceneManagement {
    public class SceneTransitionsList {
        private HashSet<SceneTransitionInfo> activatedTransitionScenes = new HashSet<SceneTransitionInfo>();

        public void ActivateSceneTransition(SceneLoader sceneLoader, UnityAction<AsyncOperation, SceneLoader> SceneChangeCallback) {
            if (activatedTransitionScenes.Contains(sceneLoader.SceneTransitionInfo)) {
                SceneChangeCallback.Invoke(null, sceneLoader);
                return;
            }

            AsyncOperation loadOperation = sceneLoader.SceneTransitionInfo.LoadSceneAsync();
            loadOperation.completed += _ => SceneChangeCallback.Invoke(loadOperation, sceneLoader);
            activatedTransitionScenes.Add(sceneLoader.SceneTransitionInfo);
        }
    }
}
