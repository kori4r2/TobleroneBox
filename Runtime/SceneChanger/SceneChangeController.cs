using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public abstract class SceneChangeController : MonoBehaviour {
        public abstract void ManageSceneUnloadOperation(AsyncOperation unloadOperation);
        public abstract void ManageSceneLoadOperation(AsyncOperation loadOperation);
    }
}
