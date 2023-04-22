using UnityEngine;

namespace Toblerone.Toolbox.Examples {
    public class SerializedStringVariableObserver : MonoBehaviour {
        [SerializeField] private VariableObserver<string> stringVariableListener;

        private void OnEnable() {
            stringVariableListener.StartWatching();
        }

        private void OnDisable() {
            stringVariableListener.StopWatching();
        }
    }
}
