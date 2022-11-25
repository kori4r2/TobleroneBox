using UnityEngine;
using TMPro;

namespace Toblerone.Toolbox.Examples {
    public class IntVariableToTextUpdater : MonoBehaviour {
        [SerializeField] private IntVariable intVariable;
        private VariableObserver<int> intVariableObserver;
        [SerializeField] private TextMeshProUGUI textMesh;

        private void Awake() {
            ResetValues();
            intVariableObserver = new VariableObserver<int>(intVariable, OnVariableChanged);
        }

        private void ResetValues() {
            intVariable.Value = 0;
            textMesh.text = "0";
        }

        private void OnVariableChanged(int newValue) {
            textMesh.text = $"{newValue}";
        }

        private void OnEnable() {
            intVariableObserver.StartWatching();
        }

        private void OnDisable() {
            intVariableObserver.StopWatching();
        }
    }
}
