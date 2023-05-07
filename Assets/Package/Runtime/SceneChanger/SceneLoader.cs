using System;
using UnityEngine;

namespace Toblerone.Toolbox {
    [CreateAssetMenu(menuName = "TobleroneBox/SceneChanger/SceneLoader")]
    public class SceneLoader : ScriptableObject {
        [SerializeField] private SceneTransitionInfo sceneTransitionInfo;
        public SceneTransitionInfo SceneTransitionInfo => sceneTransitionInfo;
        public string ScenePath { get; }
        public SceneChangeController SceneChangeController => sceneTransitionInfo.SceneChangeController.Value;
        public AsyncOperation LoadSceneAsync() {
            throw new NotImplementedException();
        }

        public AsyncOperation UnloadSceneAsync() {
            throw new NotImplementedException();
        }
    }
}
