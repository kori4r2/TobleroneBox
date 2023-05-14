using UnityEngine;

namespace Toblerone.Toolbox.SceneManagement {
    public abstract class SceneChangeController : MonoBehaviour {
        public abstract void Activate();
        public abstract void Deactivate();
        public abstract void ManageSceneUnloadOperation(AsyncOperation unloadOperation);
        public abstract void ManageSceneLoadOperation(AsyncOperation loadOperation);
    }
}
