using UnityEngine;

namespace Toblerone.Toolbox.UpdateManagerSample {
    public class SpinningObjectRuntimeOnly : ManagedBehaviour {
        private IRuntimeSet<SpinningObjectRuntimeOnly> runtimeSet = null;
        public IRuntimeSet<SpinningObjectRuntimeOnly> RuntimeSet {
            get => runtimeSet;
            set {
                if (RuntimeSet != null && RuntimeSet.Contains(this))
                    RuntimeSet.RemoveElement(this);
                runtimeSet = value;
                if (isActiveAndEnabled && RuntimeSet != null && !RuntimeSet.Contains(this))
                    RuntimeSet.AddElement(this);
            }
        }
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] float spinningSpeed = 25f;
        [SerializeField] private bool automaticActivationEnabled = true;
        private Vector3 spinAxis;

        private void Start() {
            meshRenderer.material.color = Random.ColorHSV();
            spinAxis = Random.onUnitSphere;
        }

        protected override void AddToRuntimeSet() {
            if (RuntimeSet != null)
                RuntimeSet.AddElement(this);
        }

        protected override void RemoveFromRuntimeSet() {
            if (RuntimeSet != null)
                RuntimeSet.RemoveElement(this);
        }

        public override void ManagedUpdate(float deltaTime) {
            transform.Rotate(spinAxis, spinningSpeed * deltaTime, Space.Self);
        }

        protected override void OnEnable() {
            bool wasUpdating = ShouldUpdate;
            base.OnEnable();
            if (!automaticActivationEnabled)
                ShouldUpdate = wasUpdating;
        }

        protected override void OnDisable() {
            bool wasUpdating = ShouldUpdate;
            base.OnDisable();
            if (!automaticActivationEnabled)
                ShouldUpdate = wasUpdating;
        }

        public void StartSpinning() {
            ShouldUpdate = true;
        }

        public void StopSpinning() {
            ShouldUpdate = false;
        }
    }
}
