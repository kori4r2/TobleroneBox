using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    [CreateAssetMenu(menuName = "TobleroneBox/SceneChanger/SceneLoader")]
    public class SceneLoader : ScriptableObject {
        public string ScenePath { get; }
        public GenericVariable<SceneChangeController> SceneChangeController { get; }
        public AsyncOperation LoadSceneAsync() {
            throw new NotImplementedException();
        }

        public AsyncOperation UnloadSceneAsync() {
            throw new NotImplementedException();
        }
    }
}
