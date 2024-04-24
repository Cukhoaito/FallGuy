using System;
using UnityEngine;
using Animancer;
using FallGuy.Character.States;

namespace FallGuy.Character
{
    public sealed class Character : MonoBehaviour
    {
        [SerializeField] private LayerMask _platformLayer;
        public LayerMask PlatformLayer => _platformLayer;

        [SerializeField] private Rigidbody _rigid;
        public Rigidbody Rigidbody => _rigid;

        [SerializeField] private CapsuleCollider _collider;

        public CapsuleCollider Collider => _collider;

        [SerializeField] private AnimancerComponent _animancer;
        public AnimancerComponent Animancer => _animancer;
        [SerializeField] private CharacterBrain _brain;

        public CharacterBrain Brain
        {
            get => _brain;
            set
            {
                if (_brain == value) return;

                var oldBrain = _brain;
                _brain = value;

                if (oldBrain != null) oldBrain.Character = null;

                if (value != null) value.Character = this;
            }
        }

        [SerializeField] private CharacterParameters _parameters;
        public CharacterParameters Parameters => _parameters;

        [SerializeField] private CharacterState _defaultState;
        public CharacterState DefaultState => _defaultState;

        public readonly CharacterState.StateMachine StateMachine = new();

        public Vector3 FaceDirection
        {
            get
            {
                var faceDirection = _animancer.transform.forward;
                faceDirection.y = 0;
                return Vector3.ClampMagnitude(faceDirection, 1);
            }
        }

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

        private void Awake()
        {
            StateMachine.DefaultState = _defaultState;
        }
    }
}