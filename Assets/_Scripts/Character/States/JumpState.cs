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
            UpdateTurning();
        }

        private void FixedUpdate()
        {
            Movement();
            AddExtraGravity();
        }
        public override void OnExitState()
        {
            base.OnExitState();
            _jumpTimer = Time.time + Character.Parameters.JumpDelay;
        }

        private void UpdateTurning()
        {
            var movementDirection = Character.Brain.MovementDirection;
            if (movementDirection == default) return;

            var targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
            var trans = Character.Body.Transform;
            var angle = Mathf.SmoothDampAngle(trans.eulerAngles.y, targetAngle, ref _currentTurnVelocity,
                Character.Parameters.TurnSmoothTime);
            trans.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
        private void Movement()
        {
            var movementDirection = Character.Brain.MovementDirection;

            var speed = Character.Parameters.RunSpeed;
            var acceleration = Character.Parameters.RunAcceleration;

            var targetVelocity = movementDirection * speed;
            var currentVelocity = Character.Rigidbody.velocity;
            var accelerationVector = (targetVelocity - currentVelocity) * acceleration;

            Character.Rigidbody.AddForce(accelerationVector);
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