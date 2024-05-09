using Animancer;
using UnityEngine;

namespace FallGuy.Character.States
{
    public class JumpState : CharacterState
    {
        [SerializeField] private ClipTransition _startAnimation;
        [SerializeField] private ClipTransition _loopAnimation;
        private float _nonCheckGroundHeight;
        private bool _needCheckGround;
        private float _jumpDelay;
        private bool OverDelay => _jumpDelay <= Time.time;

        public override bool CanEnterState
            => Character.StateMachine.CurrentState != this && OverDelay;
        public override bool CanExitState => OnGround();
        private void Awake()
        {
            _startAnimation.Events.OnEnd = PlayLoopAnimation;
        }
        private void OnEnable()
        {
            var offset = 0.2f;
            _needCheckGround = false;
            _nonCheckGroundHeight = Character.transform.position.y + Character.Body.CheckGroundDistance + offset;
            Jump();
            PlayStartAnimation();
        }

        private void Update()
        {
            Turning(Character.Brain.MovementDirection);
        }

        private bool OnGround()
        {
            if (_needCheckGround) return Character.Body.OnGround;
            return false;
        }

        private void FixedUpdate()
        {
            if (!_needCheckGround)
                _needCheckGround = Character.transform.position.y > _nonCheckGroundHeight
                || Character.Rigidbody.velocity.y <= 0;

            Movement(Character.Brain.MovementDirection,
                 Character.Parameters.AirMoveSpeed,
                 Character.Parameters.Acceleration);

            if (Character.Rigidbody.velocity.y <= 0)
                AddGravity(Character.Parameters.GravityMutiplier);

        }

        public override void OnExitState()
        {
            base.OnExitState();
            _jumpDelay = Time.time + Character.Parameters.JumpDelay;
        }

        private void PlayLoopAnimation()
        {
            Character.Animancer.Play(_loopAnimation);
        }

        private void PlayStartAnimation()
        {
            Character.Animancer.Play(_startAnimation);
        }
        private void Jump()
        {
            var rb = Character.Rigidbody;
            var upVelocity = Mathf.Sqrt(
                2 * Character.Parameters.JumpHeight
                * Mathf.Abs(Physics.gravity.y));
            Character.Rigidbody.velocity = new Vector3(rb.velocity.x, upVelocity, rb.velocity.y);
        }
    }
}