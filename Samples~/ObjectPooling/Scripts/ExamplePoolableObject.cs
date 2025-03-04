using UnityEngine;

namespace Toblerone.Toolbox.ObjectPoolingSample {
    public class ExamplePoolableObject : PoolableObject {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private MeshRenderer mesh;

        public override void ResetObject() {
            Color color = Random.ColorHSV();
            color.a = 1.0f;
            mesh.material.color = color;
        }

        private void Update() {
            transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
        }
    }
}
