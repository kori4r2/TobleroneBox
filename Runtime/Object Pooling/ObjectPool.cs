using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour, IPoolableObject {
        protected abstract T ObjectPrefab { get; }
        [SerializeField] private int poolSize;
        public int PoolSize => poolSize;
        protected abstract GenericEvent<T> DespawnedObjectEvent { get; }
        private GenericEventListener<T> despawnedObjectEventListener;
        private Queue<T> objectQueue = new Queue<T>();

        private void Awake() {
            BuildPool();
            despawnedObjectEventListener = new GenericEventListener<T>(DespawnedObjectEvent, ReturnObjectToPool);
        }

        private void BuildPool() {
            for (int index = 0; index < poolSize; index++) {
                T newObject = Instantiate(ObjectPrefab, Vector3.zero, Quaternion.identity);
                ReturnObjectToPool(newObject);
            }
        }

        public void ReturnObjectToPool(T objectDespawned) {
            GameObject gameObj = objectDespawned.gameObject;
            gameObj.transform.SetParent(transform);
            gameObj.SetActive(false);
            objectQueue.Enqueue(objectDespawned);
        }

        public T InstantiateObject(Vector3 position, Quaternion rotation) {
            T instantiatedObject = objectQueue.Dequeue();
            GameObject newObj = instantiatedObject.gameObject;
            newObj.transform.SetPositionAndRotation(position, rotation);
            newObj.SetActive(true);
            instantiatedObject.InitObject();
            return instantiatedObject;
        }

        private void OnEnable() {
            despawnedObjectEventListener.StartListeningEvent();
        }

        private void OnDisable() {
            despawnedObjectEventListener.StopListeningEvent();
        }
    }
}
