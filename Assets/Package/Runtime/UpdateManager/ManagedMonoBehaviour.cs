using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public abstract class ManagedMonoBehaviour : MonoBehaviour {
        public bool ShouldUpdate { get; protected set; }

        public abstract void ManagedUpdate();

        protected virtual void Awake() {
            UpdateManager.AddBehaviour(this);
        }

        protected virtual void OnDestroy() {
            UpdateManager.RemoveBehaviour(this);
        }

        protected virtual void OnEnable() {
            ShouldUpdate = true;
        }

        protected virtual void OnDisable() {
            ShouldUpdate = false;
        }
    }
}
