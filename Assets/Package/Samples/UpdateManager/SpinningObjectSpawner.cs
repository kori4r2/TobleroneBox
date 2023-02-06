using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox.UpdateManagerSample {
    public class SpinningObjectSpawner : MonoBehaviour {
        [SerializeField] private SpinningObject prefab;
        [SerializeField] private float maxObjects = 1000f;
        [SerializeField] private float spawnRange = 100f;
        [SerializeField, Range(0.1f, 10)] private float secondsBetweenSpawns = 1f;
        [SerializeField] private bool spawnInstantly = false;

        private int objectCount = 0;
        private float timer = 0;

        private void Start() {
            if (!spawnInstantly)
                return;

            while (objectCount < maxObjects) {
                InstantiateNewObject();
            }
        }

        private void Update() {
            if (objectCount >= maxObjects)
                return;
            timer += Time.deltaTime;
            if (timer <= secondsBetweenSpawns)
                return;
            timer = 0;
            InstantiateNewObject();
        }

        public void InstantiateNewObject() {
            if (objectCount >= maxObjects)
                return;
            objectCount++;
            Instantiate(prefab, CalculateNewPosition(), Quaternion.identity, transform);
        }

        private Vector3 CalculateNewPosition() {
            return Random.insideUnitSphere * spawnRange;
        }
    }
}
