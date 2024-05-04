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
        public bool OnGround
        {
            get
            {
                var bounds = _collider.bounds;
                var isCollider = Physics.SphereCast(
                    new Ray(bounds.center, -transform.up),
                    _checkGroundDistance,
                    bounds.extents.y,
                    _groundMask,
                    QueryTriggerInteraction.Ignore
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
                    -transform.up,
                    out var slopeHit,
                    bounds.extents.y + _checkGroundDistance * 2,
                    _groundMask
                ))
                    return slopeHit.normal;
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
            Gizmos.DrawWireSphere(bounds.center - new Vector3(0, bounds.extents.y, 0), _checkGroundDistance);
        }
#endif
    }

}
