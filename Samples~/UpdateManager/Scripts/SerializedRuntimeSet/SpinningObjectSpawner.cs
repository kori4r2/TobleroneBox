using UnityEngine;

namespace Toblerone.Toolbox.UpdateManagerSample {
    public class SpinningObjectSpawner : MonoBehaviour {
        [SerializeField] private SpinningObjectManager manager;
        [SerializeField] private SpinningObject prefab;
        [SerializeField] private float maxObjects = 1000f;
        [SerializeField] private float spawnRange = 100f;
        [SerializeField, Range(0.01f, 10)] private float secondsBetweenSpawns = 1f;
        [SerializeField] private bool spawnInstantly = false;
        [SerializeField] private bool objectsSpinOnSpawn = true;

        private int objectCount = 0;
        private float timer = 0;

        private void Start() {
            if (!spawnInstantly)
                return;

            SpawnMaxObjects();
        }

        public void SpawnMaxObjects() {
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
            SpinningObject newObj = Instantiate(prefab, CalculateNewPosition(), Quaternion.identity, transform);
            if (objectsSpinOnSpawn)
                newObj.StartSpinning();
            else
                newObj.StopSpinning();
        }

        private Vector3 CalculateNewPosition() {
            return Random.insideUnitSphere * spawnRange;
        }

        public void ToggleUpdateManager() {
            if (manager)
                manager.gameObject.SetActive(!manager.gameObject.activeSelf);
        }
    }
}
