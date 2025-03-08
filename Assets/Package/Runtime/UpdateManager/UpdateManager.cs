using UnityEngine;

namespace Toblerone.Toolbox {
    public abstract class UpdateManager<T> : MonoBehaviour where T : MonoBehaviour, IManagedBehaviour {
        [SerializeField] protected bool persistOnSceneChange = false;
        protected abstract IRuntimeSet<T> RuntimeSet { get; set; }
        protected T[] behavioursArray = null;

        protected virtual void Awake() {
            if (persistOnSceneChange)
                DontDestroyOnLoad(this);
            UpdateArraysAndCallbacks();
        }

        protected void UpdateArraysAndCallbacks() {
            if (RuntimeSet == null)
                return;
            UpdateArray();
            RuntimeSet.ListenToChanges(UpdateArray);
        }

        public void ChangeRuntimeSet(IRuntimeSet<T> newSet) {
            if (RuntimeSet != null)
                RuntimeSet.StopListening(UpdateArray);
            RuntimeSet = newSet;
            UpdateArraysAndCallbacks();
        }

        protected void UpdateArray() {
            behavioursArray = RuntimeSet.ToArray();
        }

        protected virtual void OnDestroy() {
            if (RuntimeSet != null)
                RuntimeSet.StopListening(UpdateArray);
        }

        protected virtual void Update() {
            TryUpdateRegisteredObjects(Time.deltaTime);
        }

        protected virtual void TryUpdateRegisteredObjects(float deltaTime) {
            foreach (T behaviour in behavioursArray) {
                if (!behaviour || !behaviour.ShouldUpdate)
                    continue;
                behaviour.ManagedUpdate(deltaTime);
            }
        }
    }
}
