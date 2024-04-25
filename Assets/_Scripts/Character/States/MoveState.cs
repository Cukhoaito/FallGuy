using Animancer;
using Animancer.FSM;
using UnityEngine;

namespace FallGuy.Character.States
{
    public class MoveState : CharacterState
    {
        [SerializeField] private float _fadeSpeed = 3f;

        [SerializeField] private MixerTransition2D _animation;
        private float _currentTurnVelocity;
        private void OnEnable()
        {
            Character.Animancer.Play(_animation);
        }
        // public bool TookOneStep => _animation.State.Parameter.x >= 0.1 && _animation.State.Parameter.x <= 0.2;
        // public override bool CanExitState => TookOneStep;

        private void Update()
        {
            UpdateAnimation();
            UpdateTurning();
        }

        private void UpdateAnimation()
        {
            _animation.State.Parameter = Vector2.MoveTowards(
                _animation.State.Parameter,
                GetAnimationTarget(),
                _fadeSpeed * Time.deltaTime
            );
        }

        private float GetCurrentSpeed() => Character.Rigidbody.velocity.magnitude;

        private Vector2 GetAnimationTarget()
        {
            var angle = Mathf.RoundToInt(Vector2.SignedAngle(
                Character.Brain.MovementDirection.ToVector2(),
                Character.Body.FaceDirection.ToVector2()
            ));
            var maxSpeed = Character.Parameters.RunSpeed;
            var speedRatio = Mathf.InverseLerp(0, maxSpeed, GetCurrentSpeed());
            return new Vector2(speedRatio, angle == 0 ? 0 : (angle > 0 ? 1 : -1));
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


        private void FixedUpdate()
        {
            var movementDirection = Character.Brain.MovementDirection;
            if (movementDirection == default)
            {
                Character.Rigidbody.velocity = default;
                return;
            }

            var speed = Character.Parameters.RunSpeed;
            var acceleration = Character.Parameters.RunAcceleration;

            var targetVelocity = movementDirection * speed;
            var currentVelocity = Character.Rigidbody.velocity;
            var accelerationVector = (targetVelocity - currentVelocity) * acceleration;

            Character.Rigidbody.AddForce(accelerationVector);
        }
    }
}