using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    public interface IRuntimeSet<T> : IEnumerable<T> where T : MonoBehaviour {
        public int Count { get; }
        public void CopyTo(T[] array);
        public T[] ToArray();
        public void ListenToChanges(UnityAction callback);
        public void StopListening(UnityAction callback);
        public bool Contains(T element);
        public void AddElement(T newElement);
        public void RemoveElement(T elementToRemove);
        public T GetActiveElement(GameObject gameObj);
    }
}
