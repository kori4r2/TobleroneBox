using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    [System.Serializable]
    public class EventListener : IEventListener {
        [SerializeField] private EventSO eventListened = null;
        [SerializeField] private UnityEvent eventCallback = new UnityEvent();

        public EventListener(EventSO eventToListen, UnityAction response) {
            eventListened = eventToListen;
            eventCallback = new UnityEvent();
            eventCallback.AddListener(response);
        }

        public void OnEventRaised() {
            eventCallback?.Invoke();
        }

        public void StartListeningEvent() {
            if (eventListened)
                eventListened.AddListener(this);
        }

        public void StopListeningEvent() {
            if (eventListened)
                eventListened.RemoveListener(this);
        }
    }
}
