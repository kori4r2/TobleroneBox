using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    public abstract class PoolableObject : MonoBehaviour {
        [SerializeField] protected RuntimeSet<PoolableObject> runtimeSet;
        private UnityAction<PoolableObject> despawnCallback = null;

        public virtual void SetDespawnCallback(UnityAction<PoolableObject> callback) {
            despawnCallback = callback;
        }

        public virtual void Despawn() {
            if (runtimeSet)
                runtimeSet.RemoveElement(this);
            despawnCallback?.Invoke(this);
        }

        public virtual void ResetObject() {
            if (runtimeSet)
                runtimeSet.AddElement(this);
        }
    }
}
