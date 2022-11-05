using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour, IPoolableObject {
        protected abstract T ObjectPrefab { get; }
        [SerializeField] protected int poolSize;
        public int PoolSize => poolSize;
        protected abstract GenericEvent<T> DespawnedObjectEvent { get; }
        protected GenericEventListener<T> despawnedObjectEventListener;
        protected Queue<T> objectQueue = new Queue<T>();

        protected virtual void Awake() {
            BuildPool();
            despawnedObjectEventListener = new GenericEventListener<T>(DespawnedObjectEvent, ReturnObjectToPool);
        }

        protected virtual void BuildPool() {
            for (int index = 0; index < poolSize; index++) {
                T newObject = Instantiate(ObjectPrefab, Vector3.zero, Quaternion.identity);
                ReturnObjectToPool(newObject);
            }
        }

        public virtual void ReturnObjectToPool(T objectDespawned) {
            GameObject gameObj = objectDespawned.gameObject;
            gameObj.transform.SetParent(transform);
            gameObj.SetActive(false);
            objectQueue.Enqueue(objectDespawned);
        }

        public virtual T InstantiateObject(Vector3 position, Quaternion rotation) {
            T instantiatedObject = objectQueue.Dequeue();
            GameObject newObj = instantiatedObject.gameObject;
            newObj.transform.SetPositionAndRotation(position, rotation);
            newObj.SetActive(true);
            instantiatedObject.InitObject();
            return instantiatedObject;
        }

        protected virtual void OnEnable() {
            despawnedObjectEventListener.StartListeningEvent();
        }

        protected virtual void OnDisable() {
            despawnedObjectEventListener.StopListeningEvent();
        }
    }
}
