using UnityEngine;

namespace Toblerone.Toolbox.UpdateManagerSample {
    public class SpinningObject : ManagedMonoBehaviour {
        [SerializeField] private SpinningObjectRuntimeSet runtimeSet;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] float spinningSpeed = 25f;
        [SerializeField] private bool automaticActivationEnabled = true;
        private Vector3 spinAxis;

        private void Start() {
            meshRenderer.material.color = Random.ColorHSV();
            spinAxis = Random.onUnitSphere;
        }

        public override void ManagedUpdate() {
            transform.Rotate(spinAxis, spinningSpeed * Time.deltaTime, Space.Self);
        }

        protected override void OnEnable() {
            if (automaticActivationEnabled)
                base.OnEnable();
            runtimeSet.AddElement(this);
        }

        protected override void OnDisable() {
            if (automaticActivationEnabled)
                base.OnDisable();
            runtimeSet.RemoveElement(this);
        }

        public void StartSpinning() {
            ShouldUpdate = true;
        }

        public void StopSpinning() {
            ShouldUpdate = false;
        }
    }
}
