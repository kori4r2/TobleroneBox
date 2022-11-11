using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    [System.Serializable]
    public class GenericEventListener<T> : IGenericEventListener<T> {
        [SerializeField] protected GenericEvent<T> eventListened = null;
        [SerializeField] protected UnityEvent<T> eventCallback = new UnityEvent<T>();

        public GenericEventListener(GenericEvent<T> eventToListen, UnityAction<T> response) {
            eventListened = eventToListen;
            eventCallback = new UnityEvent<T>();
            eventCallback.AddListener(response);
        }

        public void OnEventRaised(T value) {
            eventCallback?.Invoke(value);
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
