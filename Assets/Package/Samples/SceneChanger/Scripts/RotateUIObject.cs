using UnityEngine;

namespace Toblerone.Toolbox.SceneChangerSample {
    public class RotateUIObject : MonoBehaviour {
        [SerializeField] private RectTransform objectToRotate;
        [SerializeField] private float anglesPerSecond;
        [SerializeField] private bool clockWise;
        private Vector3 eulerAngles;
        private float zRotation = 0;

        void Start() {
            anglesPerSecond = Mathf.Abs(anglesPerSecond);
            if (objectToRotate == null)
                return;
            eulerAngles = objectToRotate.localEulerAngles;
            zRotation = eulerAngles.z;
        }

        void Update() {
            if (objectToRotate == null)
                return;
            float zRotationDelta = anglesPerSecond * (clockWise ? -1 : 1) * Time.deltaTime;
            zRotation += zRotationDelta % 360;
            transform.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, zRotation);
        }
    }
}
