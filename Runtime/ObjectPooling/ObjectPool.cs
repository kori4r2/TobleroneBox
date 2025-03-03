using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public abstract class ObjectPool<T> : MonoBehaviour where T : PoolableObject {
        protected abstract T ObjectPrefab { get; }
        [SerializeField] protected int poolSize = 10;
        public int PoolSize => poolSize;
        private int poolIncrementSize = 0;
        protected Queue<T> objectQueue = new Queue<T>();
        protected HashSet<T> spawnedObjects = new HashSet<T>();

        protected virtual void Awake() {
            BuildPool();
        }

        protected virtual void BuildPool() {
            for (int index = 0; index < poolSize; index++) {
                InstantiateNewPoolObject();
            }
        }

        protected void InstantiateNewPoolObject() {
            T newObject = Instantiate(ObjectPrefab, Vector3.zero, Quaternion.identity);
            newObject.SetDespawnCallback(ReturnObjectToPool);
            ReturnObjectToPool(newObject);
        }

        public virtual void ReturnObjectToPool(PoolableObject objectDespawned) {
            GameObject gameObj = objectDespawned.gameObject;
            gameObj.transform.SetParent(transform);
            gameObj.SetActive(false);
            spawnedObjects.Remove((T)objectDespawned);
            objectQueue.Enqueue((T)objectDespawned);
        }

        public virtual void ReturnAllObjectsToPool() {
            foreach (T obj in new List<T>(spawnedObjects)) {
                ReturnObjectToPool(obj);
            }
        }

        public virtual T InstantiateObject(Transform transform) { return InstantiateObject(transform.position, transform.rotation); }
        public virtual T InstantiateObject(Vector3 position) { return InstantiateObject(position, Quaternion.identity); }
        public virtual T InstantiateObject(Vector3 position, Quaternion rotation) {
            if (objectQueue.Count <= 0)
                ExpandPool();
            T instantiatedObject = objectQueue.Dequeue();
            GameObject newObj = instantiatedObject.gameObject;
            newObj.transform.SetPositionAndRotation(position, rotation);
            instantiatedObject.ResetObject();
            newObj.SetActive(true);
            spawnedObjects.Add(instantiatedObject);
            return instantiatedObject;
        }

        protected abstract void ExpandPool();

        protected void ExpandPoolByFixedNumber(int increment) {
            for (int count = 0; count < increment; count++) {
                InstantiateNewPoolObject();
            }
            poolSize += increment;
        }

        protected void ExpandPoolByCurrentSize() {
            BuildPool();
            poolSize += poolSize;
        }

        protected void ExpandPoolArithmeticProgression(int commonDifference) {
            poolIncrementSize += commonDifference;
            ExpandPoolByFixedNumber(poolIncrementSize);
        }
    }
}
