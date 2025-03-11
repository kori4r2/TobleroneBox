using UnityEngine;

namespace Toblerone.Toolbox.ObjectPoolingSample {
    public class ExampleObjectPool : ObjectPool<ExamplePoolableObject> {
        [SerializeField] private ExamplePoolableObject prefab;
        protected override ExamplePoolableObject ObjectPrefab => prefab;
        [SerializeField, Range(1, 500)] private int poolExpansionSize = 1;

        protected override void ExpandPool() {
            ExpandPoolByFixedNumber(poolExpansionSize);
        }
    }
}
