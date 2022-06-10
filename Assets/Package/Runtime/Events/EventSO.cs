using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    [CreateAssetMenu(menuName = "Events/VoidEvent")]
    public class EventSO : ScriptableObject {
        private List<IEventListener> listeners = new List<IEventListener>();

        public void AddListener(IEventListener newListener) {
            if (!listeners.Contains(newListener)) {
                listeners.Add(newListener);
            }
        }

        public void RemoveListener(IEventListener newListener) {
            if (listeners.Contains(newListener)) {
                listeners.Remove(newListener);
            }
        }

        public void Raise() {
            foreach (IEventListener listener in listeners) {
                if (listener != null)
                    listener.OnEventRaised();
            }
        }
    }
}
