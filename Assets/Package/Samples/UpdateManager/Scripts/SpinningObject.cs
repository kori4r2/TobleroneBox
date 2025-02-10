using UnityEngine;

namespace Toblerone.Toolbox.UpdateManagerSample {
    public class SpinningObject : ManagedBehaviour {
        [SerializeField] protected SpinningObjectRuntimeSet runtimeSet;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] float spinningSpeed = 25f;
        [SerializeField] private bool automaticActivationEnabled = true;
        private Vector3 spinAxis;

        private void Start() {
            meshRenderer.material.color = Random.ColorHSV();
            spinAxis = Random.onUnitSphere;
        }

        protected override void AddToRuntimeSet() {
            runtimeSet.AddElement(this);
        }

        protected override void RemoveFromRuntimeSet() {
            runtimeSet.RemoveElement(this);
        }

        public override void ManagedUpdate() {
            transform.Rotate(spinAxis, spinningSpeed * Time.deltaTime, Space.Self);
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
