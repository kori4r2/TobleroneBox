using UnityEngine;

namespace Toblerone.Toolbox.Examples {
    public class ValueUpdateEnabler : MonoBehaviour {
        [SerializeField] VariableObserver<bool> stopWatchIsStoppedObserver;

        private void OnEnable() {
            stopWatchIsStoppedObserver.StartWatching();
        }

        private void OnDisable() {
            stopWatchIsStoppedObserver.StopWatching();
        }
    }
}
