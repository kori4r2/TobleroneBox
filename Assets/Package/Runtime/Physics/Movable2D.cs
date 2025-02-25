using UnityEngine;

namespace Toblerone.Toolbox {
    [System.Serializable]
    public class Movable2D {
        [SerializeField] private Rigidbody2D movableRigidbody = null;
        public bool IsMoving => CanMove && movableRigidbody.velocity.magnitude > Mathf.Epsilon;
        public bool CanMove => movableRigidbody.bodyType != RigidbodyType2D.Static;
        private bool shouldUpdateVelocity = false;
        private Vector2 newVelocity = Vector2.zero;
        public Vector2 CurrentVelocity => movableRigidbody.velocity;

        public Movable2D(Rigidbody2D movableRigid) {
            movableRigidbody = movableRigid;
        }

        public void BlockMovement() {
            movableRigidbody.bodyType = RigidbodyType2D.Static;
            shouldUpdateVelocity = false;
        }

        public void AllowDynamicMovement() {
            movableRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        public void AllowKinematicMovement() {
            movableRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        public void SetVelocity(Vector2 velocity) {
            if (!CanMove)
                return;

            shouldUpdateVelocity = true;
            newVelocity = velocity;
        }

        public void UpdateMovable() {
            UpdateVelocity();
        }

        private void UpdateVelocity() {
            if (shouldUpdateVelocity)
                movableRigidbody.velocity = newVelocity;
            shouldUpdateVelocity = false;
        }
    }
}
