using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    public abstract class RuntimeSet<T> : ScriptableObject, IEnumerable<T> where T : MonoBehaviour {
        protected Dictionary<GameObject, T> activeObjsDictionary = new Dictionary<GameObject, T>();
        protected HashSet<T> activeObjectsHashSet = new HashSet<T>();
        protected UnityEvent onChange = new UnityEvent();

        public int Count => activeObjectsHashSet.Count;

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator() {
            return activeObjectsHashSet.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            return activeObjectsHashSet.GetEnumerator();
        }

        public void CopyTo(T[] array) {
            activeObjectsHashSet.CopyTo(array);
        }

        public T[] ToArray() {
            T[] array = new T[Count];
            CopyTo(array);
            return array;
        }
        #endregion

        public void ListenToChanges(UnityAction callback) {
            onChange.AddListener(callback);
        }

        public void StopListening(UnityAction callback) {
            onChange.RemoveListener(callback);
        }

        public bool Contains(T element) {
            return activeObjectsHashSet.Contains(element);
        }

        public void AddElement(T newElement) {
            if (Contains(newElement))
                return;

            GameObject newElementObj = newElement.gameObject;
            if (activeObjsDictionary.ContainsKey(newElementObj)) {
                Debug.LogWarning($"RuntimeSet {name} already contains an element from object {newElementObj}", this);
                return;
            }

            activeObjsDictionary.Add(newElementObj, newElement);
            activeObjectsHashSet.Add(newElement);
            onChange.Invoke();
        }

        public void RemoveElement(T elementToRemove) {
            if (!Contains(elementToRemove))
                return;

            GameObject elementObj = elementToRemove.gameObject;
            activeObjsDictionary.Remove(elementObj);
            activeObjectsHashSet.Remove(elementToRemove);
            onChange.Invoke();
        }

        public T GetActiveElement(GameObject gameObj) {
            if (!activeObjsDictionary.ContainsKey(gameObj))
                return null;

            return activeObjsDictionary[gameObj];
        }
    }
}
