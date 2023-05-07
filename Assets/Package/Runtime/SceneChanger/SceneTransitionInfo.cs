using System;
using UnityEngine;

namespace Toblerone.Toolbox {
    [CreateAssetMenu(menuName = "TobleroneBox/SceneChanger/SceneTransitionInfo")]
    public class SceneTransitionInfo : ScriptableObject {
        [SerializeField] private ScenePicker transitionScene;
        [SerializeField] private SceneChangeControllerVariable sceneChangeController;
        public GenericVariable<SceneChangeController> SceneChangeController => sceneChangeController;
        public AsyncOperation LoadSceneAsync() {
            throw new NotImplementedException();
        }
    }
}
