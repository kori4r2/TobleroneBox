using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox.UpdateManagerSample {
    public class SpinningObject : ManagedMonoBehaviour {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] float spinningSpeed = 25f;
        private Vector3 spinAxis;

        private void Start() {
            meshRenderer.material.color = Random.ColorHSV();
            spinAxis = Random.onUnitSphere;
        }

        public override void ManagedUpdate() {
            transform.Rotate(spinAxis, spinningSpeed * Time.deltaTime, Space.Self);
        }
    }
}
