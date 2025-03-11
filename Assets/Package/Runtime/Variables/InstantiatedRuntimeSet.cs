using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    public abstract class InstantiatedRuntimeSet<T> : IRuntimeSet<T> where T : MonoBehaviour {
        protected HashSet<T> hashSet = new HashSet<T>();
        protected Dictionary<GameObject, T> gameObjDictionary = new Dictionary<GameObject, T>();
        public int Count => hashSet.Count;
        protected UnityEvent onChange = new UnityEvent();

        #region IEnumerable
        public IEnumerator<T> GetEnumerator() {
            return hashSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return hashSet.GetEnumerator();
        }

        public void CopyTo(T[] array) {
            hashSet.CopyTo(array);
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
            return hashSet.Contains(element);
        }

        public void AddElement(T newElement) {
            if (Contains(newElement))
                return;

            GameObject newElementObj = newElement.gameObject;
            if (gameObjDictionary.ContainsKey(newElementObj)) {
                Debug.LogWarning($"Instantiated RuntimeSet already contains an element from object {newElementObj}");
                return;
            }

            gameObjDictionary.Add(newElementObj, newElement);
            hashSet.Add(newElement);
            onChange.Invoke();
        }

        public void RemoveElement(T elementToRemove) {
            if (!Contains(elementToRemove))
                return;

            GameObject elementObj = elementToRemove.gameObject;
            gameObjDictionary.Remove(elementObj);
            hashSet.Remove(elementToRemove);
            onChange.Invoke();
        }

        public void Clear() {
            hashSet.Clear();
        }

        public T GetActiveElement(GameObject gameObj) {
            if (!gameObjDictionary.ContainsKey(gameObj))
                return null;

            return gameObjDictionary[gameObj];
        }
    }
}
