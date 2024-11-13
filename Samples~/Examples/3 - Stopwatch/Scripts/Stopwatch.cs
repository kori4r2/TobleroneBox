using UnityEngine;

namespace Toblerone.Toolbox.Examples {
    public class Stopwatch : MonoBehaviour {
        [SerializeField] private FloatVariable timer;
        [SerializeField] private EventSO runStopwatchEvent;
        [SerializeField] private EventSO stopStopwatchEvent;
        [SerializeField] private EventSO stopwatchZeroEvent;
        [SerializeField] private BoolVariable isStopped;
        private EventListener runStopwatchEventListener;
        private EventListener stopStopwatchEventListener;

        private void Start() {
            timer.Value = 0f;
            isStopped.Value = true;
        }

        private void Awake() {
            runStopwatchEventListener = new EventListener(runStopwatchEvent, () => isStopped.Value = false);
            stopStopwatchEventListener = new EventListener(stopStopwatchEvent, () => isStopped.Value = true);
        }

        private void OnEnable() {
            runStopwatchEventListener.StartListeningEvent();
            stopStopwatchEventListener.StartListeningEvent();
        }

        private void OnDisable() {
            runStopwatchEventListener.StopListeningEvent();
            stopStopwatchEventListener.StopListeningEvent();
        }

        private void Update() {
            if (isStopped.Value) {
                if (timer.Value <= 0)
                    timer.Value = 0;
                return;
            }

            timer.Value -= Time.deltaTime;
            if (timer.Value <= 0) {
                isStopped.Value = true;
                stopwatchZeroEvent.Raise();
            }
        }
    }
}
