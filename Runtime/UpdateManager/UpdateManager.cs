using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public class UpdateManager : MonoBehaviour {
        private static UpdateManager instance;
        public static UpdateManager Instance {
            get {
                if (instance)
                    return instance;
                GameObject emptyObject = new GameObject("UpdateManager");
                instance = emptyObject.AddComponent<UpdateManager>();
                return instance;
            }
            private set => instance = value;
        }
        private HashSet<ManagedMonoBehaviour> behaviours = new HashSet<ManagedMonoBehaviour>();
        private ManagedMonoBehaviour[] behavioursArray;
        private bool hashSetAltered = true;

        private void Awake() {
            if (Instance != null) {
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this);
        }

        public void AddBehaviour(ManagedMonoBehaviour newBehaviour) {
            if (behaviours.Contains(newBehaviour))
                return;
            behaviours.Add(newBehaviour);
            hashSetAltered = true;
        }

        public void RemoveBehaviour(ManagedMonoBehaviour newBehaviour) {
            if (!behaviours.Contains(newBehaviour))
                return;
            behaviours.Remove(newBehaviour);
            hashSetAltered = true;
        }

        private void Update() {
            if (hashSetAltered) {
                UpdateArray();
                hashSetAltered = false;
            }
            TryUpdateRegisteredObjects();
        }

        private void UpdateArray() {
            behavioursArray = new ManagedMonoBehaviour[behaviours.Count];
            behaviours.CopyTo(behavioursArray);
        }

        private void TryUpdateRegisteredObjects() {
            foreach (ManagedMonoBehaviour behaviour in behavioursArray) {
                if (!behaviour || !behaviour.ShouldUpdate)
                    continue;
                behaviour.ManagedUpdate();
            }
        }
    }
}
