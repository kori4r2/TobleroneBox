using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public class UpdateManager : MonoBehaviour {
        private static UpdateManager instance = null;
        private static UpdateManager Instance {
            get {
                if (instance)
                    return instance;
                GameObject emptyObject = new GameObject("UpdateManager");
                emptyObject.transform.SetParent(null);
                instance = emptyObject.AddComponent<UpdateManager>();
                return instance;
            }
        }
        [SerializeField] private bool persistOnSceneChange = false;
        private HashSet<ManagedMonoBehaviour> behaviours = new HashSet<ManagedMonoBehaviour>();
        private ManagedMonoBehaviour[] behavioursArray;
        private bool hashSetAltered = true;

        private void Awake() {
            if (instance != null) {
                Destroy(this);
                return;
            }
            if (persistOnSceneChange)
                DontDestroyOnLoad(this);
            instance = this;
        }

        public static void AddBehaviour(ManagedMonoBehaviour newBehaviour) {
            if (Instance.behaviours.Contains(newBehaviour))
                return;
            Instance.behaviours.Add(newBehaviour);
            Instance.hashSetAltered = true;
        }

        public static void RemoveBehaviour(ManagedMonoBehaviour newBehaviour) {
            if (!instance || !Instance.behaviours.Contains(newBehaviour))
                return;
            Instance.behaviours.Remove(newBehaviour);
            Instance.hashSetAltered = true;
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
