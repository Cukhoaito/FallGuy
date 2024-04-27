using Animancer;
using Animancer.Units;
using UnityEngine;

namespace FallGuy.Character.States
{
    public class JumpState : CharacterState
    {
        [SerializeField] private ClipTransition _startAnimation;
        [SerializeField] private ClipTransition _loopAnimation;
        [SerializeField, Seconds, Min(0)] private float _coyoteTime = 0.3f;
        private float _jumpTimer;
        private float _currentTurnVelocity;
        private bool CanJump => _jumpTimer <= Time.time;

        public float CoyoteTime => _coyoteTime;
        public override bool CanEnterState
            => Character.StateMachine.CurrentState != this && CanJump;
        public override bool CanExitState
            => Character.Body.OnGround && Character.Rigidbody.velocity.y <= 0;


        private void OnEnable()
        {
            var velocity = Character.Rigidbody.velocity;
            Character.Rigidbody.velocity =
                new Vector3(velocity.x, Character.Parameters.JumpForce, velocity.z);
            PlayStartAnimation();
        }
        private void Update()
        {
            Turning(Character.Brain.MovementDirection);
        }

        private void FixedUpdate()
        {
            ExtraGravity(Character.Parameters.GravityMutipler);
            Movement(Character.Brain.MovementDirection,
                Character.Parameters.RunSpeed,
                Character.Parameters.RunAcceleration);
        }
        public override void OnExitState()
        {
            base.OnExitState();
            _jumpTimer = Time.time + Character.Parameters.JumpDelay;
        }

        private void PlayLoopAnimation()
        {
            var state = Character.Animancer.Play(_loopAnimation);
            state.Events.OnEnd = PlayStartAnimation;
        }

        private void PlayStartAnimation()
        {
            var state = Character.Animancer.Play(_startAnimation);
            state.Events.OnEnd = PlayLoopAnimation;
        }
    }
}