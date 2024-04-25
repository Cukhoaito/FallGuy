using UnityEngine;
namespace FallGuy.Character
{
    public class CharacterBody : MonoBehaviour
    {
        [SerializeField] private LayerMask _platformLayer;
        public LayerMask PlatformLayer => _platformLayer;
        [SerializeField] private Collider _collider;
        public Collider Collider => _collider;

        public Transform Transform => transform;
        public bool IsGrounded
        {
            get
            {
                const float boxHeight = .05f;
                var bounds = _collider.bounds;

                var boxCenter = bounds.center;
                var halfExtents = bounds.extents;

                halfExtents.y = boxHeight;

                var maxDistance = bounds.extents.y;
                var isCollider = Physics.BoxCast(
                    boxCenter,
                    halfExtents,
                    Vector3.down,
                    transform.rotation,
                    maxDistance,
                    _platformLayer
                );

                return isCollider;
            }
        }
        public Vector3 FaceDirection
        {
            get
            {
                var faceDirection = transform.forward;
                faceDirection.y = 0;
                return Vector3.ClampMagnitude(faceDirection, 1);
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            const float boxHeight = .05f;
            var color = IsGrounded ? Color.green : Color.red;
            Gizmos.color = color;
            var bounds = _collider.bounds;

            var boxCenter = bounds.center;
            var halfExtents = bounds.extents;
            var extentY = halfExtents.y;
            halfExtents.y = boxHeight;
            Gizmos.DrawWireCube(boxCenter - new Vector3(0, extentY + boxHeight, 0), halfExtents * 2);
        }
#endif
    }

}
