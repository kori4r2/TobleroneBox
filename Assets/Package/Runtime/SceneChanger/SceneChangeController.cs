using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox.SceneManagement {
    public abstract class SceneChangeController : MonoBehaviour {
        public abstract void Activate(UnityAction onPrepared);
        public abstract void Deactivate(UnityAction onFinish);
        public abstract void DisplaySceneLoadOperation(AsyncOperation loadOperation);
    }
}
