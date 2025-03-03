using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    public abstract class PoolableObject : MonoBehaviour {
        private UnityAction<PoolableObject> despawnCallback = null;

        public virtual void SetDespawnCallback(UnityAction<PoolableObject> callback) {
            despawnCallback = callback;
        }

        public virtual void Despawn() { despawnCallback?.Invoke(this); }

        public abstract void ResetObject();
    }
}
