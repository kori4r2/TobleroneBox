using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public class EventsResponseList : MonoBehaviour {
        [SerializeField] private List<EventListener> eventListeners = new List<EventListener>();

        private void OnEnable() {
            foreach (EventListener listener in eventListeners) {
                listener.StartListeningEvent();
            }
        }

        private void OnDisable() {
            foreach (EventListener listener in eventListeners) {
                listener.StopListeningEvent();
            }
        }
    }
}
