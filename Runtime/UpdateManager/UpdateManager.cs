using UnityEngine;

namespace Toblerone.Toolbox {
    public class UpdateManager<T> : MonoBehaviour where T : MonoBehaviour, IManagedBehaviour {
        [SerializeField] private bool persistOnSceneChange = false;
        [SerializeField] private RuntimeSet<T> behaviours;
        private T[] behavioursArray = null;

        protected virtual void Awake() {
            if (persistOnSceneChange)
                DontDestroyOnLoad(this);
            UpdateArray();
            behaviours.ListenToChanges(UpdateArray);
        }

        private void UpdateArray() {
            behavioursArray = behaviours.ToArray();
        }

        protected virtual void OnDestroy() {
            behaviours.StopListening(UpdateArray);
        }

        protected virtual void Update() {
            TryUpdateRegisteredObjects();
        }

        protected virtual void TryUpdateRegisteredObjects() {
            foreach (T behaviour in behavioursArray) {
                if (!behaviour || !behaviour.ShouldUpdate)
                    continue;
                behaviour.ManagedUpdate();
            }
        }
    }
}
