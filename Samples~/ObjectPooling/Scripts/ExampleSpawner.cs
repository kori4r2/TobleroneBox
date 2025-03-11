using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox.ObjectPoolingSample {
    public class ExampleSpawner : MonoBehaviour {
        [SerializeField, Range(1.0f, 500f)] private float spawnRadius = 5.0f;
        [SerializeField] private ExampleSpawnerReference reference;
        [SerializeField] private ExampleObjectPool pool;
        private List<ExamplePoolableObject> spawnedObjects = new List<ExamplePoolableObject>();

        private void OnDrawGizmosSelected() {
            Gizmos.color = new Color(0, 1.0f, 0, 0.5f);
            Gizmos.DrawSphere(transform.position, spawnRadius);
        }

        private void Awake() {
            reference.Value = this;
        }

        private void OnDestroy() {
            if (reference.Value == this)
                reference.Value = null;
        }

        public void SpawnNewObject() {
            spawnedObjects.Add(pool.InstantiateObject(spawnRadius * Random.insideUnitSphere, Random.rotation));
        }

        public void SpawnMultipleObjects(int count) {
            for (int i = 0; i < count; i++)
                SpawnNewObject();
        }

        public void DespawnRandomObject() {
            if (spawnedObjects.Count < 1)
                return;
            ExamplePoolableObject despawned = spawnedObjects[Random.Range(0, spawnedObjects.Count)];
            pool.ReturnObjectToPool(despawned);
            spawnedObjects.Remove(despawned);
        }

        public void DespawnMultipleRandomObjects(int count) {
            for (int i = 0; i < count && spawnedObjects.Count > 0; i++)
                DespawnRandomObject();
        }

        public void DespawnAllObjects() {
            spawnedObjects.Clear();
            pool.ReturnAllObjectsToPool();
        }
    }
}
