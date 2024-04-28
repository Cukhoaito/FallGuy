using UnityEngine;
namespace FallGuy.Character
{
    public class CharacterBody : MonoBehaviour
    {
        [Header("Ground Detection")]
        [SerializeField] private LayerMask _platformLayer;
        public LayerMask PlatformLayer => _platformLayer;
        [SerializeField, Min(0)] private float _groundDistance = 0.05f;
        public float GroundDistance => _groundDistance;
        [Header("Collider")]
        [SerializeField] private Collider _collider;
        public Collider Collider => _collider;
        public Transform Transform => transform;
        public bool OnGround
        {
            get
            {
                var bounds = _collider.bounds;

                var halfExtents = bounds.extents / Mathf.Sqrt(2);
                halfExtents.y = _groundDistance;

                var maxDistance = bounds.extents.y + _groundDistance;

                var isCollider = Physics.BoxCast(
                    bounds.center,
                    halfExtents,
                    Vector3.down,
                    Transform.rotation,
                    maxDistance,
                    _platformLayer
                );

                return isCollider;
            }
        }

        public Vector3 SurfaceNormal
        {
            get
            {
                var bounds = _collider.bounds;
                if (Physics.Raycast(
                    bounds.center,
                    Vector3.down,
                    out var slopeHit,
                    bounds.extents.y + _groundDistance
                ))
                {
                    return slopeHit.normal;
                }
                return default;
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
            var color = OnGround ? Color.green : Color.red;
            Gizmos.color = color;
            var bounds = _collider.bounds;

            var halfExtents = bounds.extents;
            var extentY = halfExtents.y;

            halfExtents /= Mathf.Sqrt(2);
            halfExtents.y = _groundDistance;

            Gizmos.DrawWireCube(bounds.center - new Vector3(0, extentY + _groundDistance, 0), 2 * halfExtents);
        }
#endif
    }

}
