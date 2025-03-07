using UnityEngine;
using UnityEngine.Events;

namespace Toblerone.Toolbox {
    public abstract class PoolableObject : MonoBehaviour, IPoolableObject {
        protected UnityAction<IPoolableObject> despawnCallback = null;
        public GameObject GameObject => gameObject;

        public virtual void SetDespawnCallback(UnityAction<IPoolableObject> callback) {
            despawnCallback = callback;
        }

        public virtual void Despawn() { despawnCallback?.Invoke(this); }

        public abstract void ResetObject();
    }
}
