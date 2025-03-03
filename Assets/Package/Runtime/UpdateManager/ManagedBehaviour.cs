using UnityEngine;

namespace Toblerone.Toolbox {
    public abstract class ManagedBehaviour : MonoBehaviour {
        public bool ShouldUpdate { get; protected set; }
        protected abstract void AddToRuntimeSet();
        protected abstract void RemoveFromRuntimeSet();

        public abstract void ManagedUpdate();

        protected virtual void OnEnable() {
            ShouldUpdate = true;
            AddToRuntimeSet();
        }

        protected virtual void OnDisable() {
            ShouldUpdate = false;
            RemoveFromRuntimeSet();
        }
    }
}
