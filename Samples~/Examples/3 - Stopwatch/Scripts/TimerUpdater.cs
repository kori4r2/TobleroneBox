using System;
using UnityEngine;
using TMPro;

namespace Toblerone.Toolbox.Examples {
    public class TimerUpdater : MonoBehaviour {
        [SerializeField] private FloatVariable timeVariable;
        [SerializeField] private TextMeshProUGUI textField;
        private VariableObserver<float> timeObserver;

        private void Awake() {
            timeObserver = new VariableObserver<float>(timeVariable, UpdateTimer);
        }

        private void UpdateTimer(float newTimeSeconds) {
            int totalSeconds = Mathf.FloorToInt(Mathf.Max(0f, newTimeSeconds));
            int milliseconds = Mathf.Clamp(Mathf.CeilToInt(1000f * (newTimeSeconds - totalSeconds)), 0, 999);
            int seconds = 0;
            int minutes = Math.DivRem(totalSeconds, 60, out seconds);
            textField.text = $"{minutes:00}:{seconds:00}.{milliseconds:000}";
        }

        private void OnEnable() {
            timeObserver.StartWatching();
        }

        private void OnDisable() {
            timeObserver.StopWatching();
        }
    }
}
