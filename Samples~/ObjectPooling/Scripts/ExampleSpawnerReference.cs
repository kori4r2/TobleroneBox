using UnityEngine;

namespace Toblerone.Toolbox.ObjectPoolingSample {
    [CreateAssetMenu(menuName = "TobleroneBox/Samples/ExampleSpawnerReference")]
    public class ExampleSpawnerReference : GenericVariable<ExampleSpawner> {
        public void SpawnNewObject() { Value.SpawnNewObject(); }

        public void SpawnMultipleObjects(int count) { Value.SpawnMultipleObjects(count); }

        public void DespawnRandomObject() { Value.DespawnRandomObject(); }
        public void DespawnMultipleRandomObject(int count) { Value.DespawnMultipleRandomObjects(count); }

        public void DespawnAllObjects() { Value.DespawnAllObjects(); }
    }
}
