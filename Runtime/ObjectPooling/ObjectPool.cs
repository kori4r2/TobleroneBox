using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour, IPoolableObject {
        protected const string expandTooltip = "Expand pool by one object at a time or by Pool Size when over the limit";
        protected abstract T ObjectPrefab { get; }
        [SerializeField] protected int poolSize;
        [SerializeField, Tooltip(expandTooltip)] protected bool expandOneByOne = true;
        [SerializeField] protected GenericVariable<ObjectPool<T>> reference;
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
                InstantiateNewPoolObject();
            }
        }

        protected void InstantiateNewPoolObject() {
            T newObject = Instantiate(ObjectPrefab, Vector3.zero, Quaternion.identity);
            ReturnObjectToPool(newObject);
        }

        public virtual void ReturnObjectToPool(T objectDespawned) {
            GameObject gameObj = objectDespawned.gameObject;
            gameObj.transform.SetParent(transform);
            gameObj.SetActive(false);
            objectQueue.Enqueue(objectDespawned);
        }

        public virtual T InstantiateObject(Vector3 position, Quaternion rotation) {
            if (objectQueue.Count <= 0)
                ExpandPool();
            T instantiatedObject = objectQueue.Dequeue();
            GameObject newObj = instantiatedObject.gameObject;
            newObj.transform.SetPositionAndRotation(position, rotation);
            newObj.SetActive(true);
            instantiatedObject.InitObject();
            return instantiatedObject;
        }

        protected void ExpandPool() {
            if (expandOneByOne) {
                InstantiateNewPoolObject();
            } else {
                BuildPool();
                poolSize += poolSize;
            }
        }

        protected virtual void OnEnable() {
            despawnedObjectEventListener.StartListeningEvent();
        }

        protected virtual void OnDisable() {
            despawnedObjectEventListener.StopListeningEvent();
        }
    }
}
