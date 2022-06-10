using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public abstract class RuntimeSet<T> : ScriptableObject, IEnumerable<T> where T : MonoBehaviour {
        private Dictionary<GameObject, T> activeObjsDictionary = new Dictionary<GameObject, T>();
        private HashSet<T> activeObjectsHashSet = new HashSet<T>();

        public int Count => activeObjectsHashSet.Count;

        #region IEnumerable
        IEnumerator IEnumerable.GetEnumerator() {
            return activeObjectsHashSet.GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() {
            return activeObjectsHashSet.GetEnumerator();
        }

        public T[] ToArray() {
            T[] array = new T[Count];
            activeObjectsHashSet.CopyTo(array);
            return array;
        }
        #endregion

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
        }

        public void RemoveElement(T elementToRemove) {
            if (!Contains(elementToRemove))
                return;

            GameObject elementObj = elementToRemove.gameObject;
            activeObjsDictionary.Remove(elementObj);
            activeObjectsHashSet.Remove(elementToRemove);
        }

        public T GetActiveElement(GameObject gameObj) {
            if (!activeObjsDictionary.ContainsKey(gameObj))
                return null;

            return activeObjsDictionary[gameObj];
        }
    }
}
