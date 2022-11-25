using System.Collections.Generic;
using UnityEngine;

namespace Toblerone.Toolbox {
    public class CameraEdgeCollider : MonoBehaviour {
        #region Static Variables and Properties
        public const string boundsTag = "Bounds";
        private static Vector2 FirstCorner => new Vector2(-1, 1);
        private static Vector2 SecondCorner => new Vector2(-1, -1);
        private static Vector2 ThirdCorner => new Vector2(1, -1);
        private static Vector2 FourthCorner => new Vector2(1, 1);
        private static Vector2[] defaultColliderPoints = null;
        private static Vector2[] DefaultColliderPoints {
            get {
                defaultColliderPoints ??= new Vector2[] { FirstCorner, SecondCorner, ThirdCorner, FourthCorner, FirstCorner };
                return defaultColliderPoints;
            }
        }
        #endregion
        [SerializeField, Range(0.1f, 2f)] private float edgeRadius = 0.25f;
        [SerializeField, Range(0f, 10f)] private float padding = 2f;
        [SerializeField] private Camera boundsCamera;
        [SerializeField] private EdgeCollider2D boundsCollider;
        [SerializeField] private EventSO cameraChangeEvent;
        private EventListener cameraChangeEventListener;

        private void Awake() {
            cameraChangeEventListener = new EventListener(cameraChangeEvent, UpdateBounds);
        }

        private void UpdateBounds() {
            Vector2 worldSpaceLimits = GetWorldSpaceLimits();
            UpdateEdgeCollider(worldSpaceLimits);
        }

        private Vector2 GetWorldSpaceLimits() {
            Vector2 worldSpaceLimits = CameraUtils.GetWorldSpaceCameraSize(boundsCamera);
            worldSpaceLimits += new Vector2(2 * edgeRadius, 2 * edgeRadius);
            worldSpaceLimits += new Vector2(2 * padding, 2 * padding);
            return worldSpaceLimits;
        }

        private void UpdateEdgeCollider(Vector2 worldSpaceLimits) {
            boundsCollider.transform.position = boundsCamera.transform.position;
            boundsCollider.edgeRadius = edgeRadius;
            List<Vector2> newPoints = GetNewColliderPoints(worldSpaceLimits);
            boundsCollider.SetPoints(newPoints);
        }

        private static List<Vector2> GetNewColliderPoints(Vector2 worldSpaceLimits) {
            List<Vector2> newPoints = new List<Vector2>(DefaultColliderPoints);
            for (int index = 0; index < DefaultColliderPoints.Length; index++) {
                newPoints[index] *= worldSpaceLimits / 2f;
            }
            return newPoints;
        }

        private void OnEnable() {
            cameraChangeEventListener?.StartListeningEvent();
        }

        private void OnDisable() {
            cameraChangeEventListener?.StopListeningEvent();
        }

        private void Start() {
            UpdateBounds();
        }
    }
}
