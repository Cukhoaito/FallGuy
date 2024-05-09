using UnityEngine;
namespace FallGuy.Character
{
    public class CharacterBody : MonoBehaviour
    {
        [Header("Ground Detection")]
        [SerializeField] private LayerMask _groundMask;
        public LayerMask GroundMask => _groundMask;
        [SerializeField, Min(0)] private float _checkGroundDistance = 0.05f;
        public float CheckGroundDistance => _checkGroundDistance;
        [Header("Collider")]
        [SerializeField] private CapsuleCollider _collider;
        public CapsuleCollider Collider => _collider;
        public Transform Transform => transform;
        private RaycastHit _groundHit;
        public bool OnGround
        {
            get
            {
                var bounds = _collider.bounds;
                var isCollider = Physics.SphereCast(
                    new Ray(bounds.center, -transform.up),
                    _checkGroundDistance,
                    out _groundHit,
                    bounds.extents.y,
                    _groundMask,
                    QueryTriggerInteraction.Ignore
                );
                return isCollider;
            }
        }

        public Vector3 FaceSurfaceNormal
        {
            get
            {
                var bounds = _collider.bounds;
                var onGround = _groundHit.collider != default;
                var direction = onGround ?
                    Vector3.ProjectOnPlane(transform.forward, GroundSurfaceNormal) : transform.forward;
                var offset = -transform.forward * 0.05f;
                var point1 = bounds.center - transform.up * (bounds.extents.y - _collider.radius) + offset;
                var point2 = bounds.center + transform.up * (bounds.extents.y - _collider.radius) + offset;

                var distance = bounds.extents.x;

                var isCollider = Physics.CapsuleCast(
                    point1, point2,
                    _collider.radius,
                    direction,
                    out var faceHit,
                    distance, _groundMask
                );

                return isCollider ? faceHit.normal : default;
            }
        }

        public Vector3 GroundSurfaceNormal => OnGround ? _groundHit.normal : default;

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
            var onGround = _groundHit.collider != default;
            var bounds = _collider.bounds;
            Gizmos.color = onGround ? Color.green : Color.red;
            Gizmos.DrawWireSphere(bounds.center - new Vector3(0, bounds.extents.y, 0), _checkGroundDistance);
            var form = onGround ? _groundHit.point : bounds.center - transform.up * bounds.extents.y;
            var direction = onGround ?
                Vector3.ProjectOnPlane(transform.forward, GroundSurfaceNormal) : transform.forward;
            Gizmos.DrawRay(form, direction);
        }
#endif
    }

}
