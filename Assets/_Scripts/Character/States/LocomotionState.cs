using Animancer;
using Animancer.FSM;
using UnityEngine;

namespace FallGuy.Character.States
{
    public class LocomotionState : CharacterState
    {
        [SerializeField] private float _fadeSpeed = 3f;

        [SerializeField] private MixerTransition2D _animation;
        private void OnEnable()
        {
            Character.Animancer.Play(_animation);
        }
        private void Update()
        {
            UpdateAnimation();
            Turning(Character.Brain.MovementDirection);
        }

        private void UpdateAnimation()
        {
            _animation.State.Parameter = Vector2.MoveTowards(
                _animation.State.Parameter,
                GetAnimationTarget(),
                _fadeSpeed * Time.deltaTime
            );
        }
        private Vector2 GetAnimationTarget()
        {
            var angle = Mathf.RoundToInt(Vector2.SignedAngle(
                Character.Brain.MovementDirection.ToVector2(),
                Character.Body.FaceDirection.ToVector2()
            ));
            var maxSpeed = Character.Parameters.RunSpeed;
            var currentSpeed = Character.Rigidbody.velocity.magnitude;
            var speedRatio = Mathf.InverseLerp(0, maxSpeed, currentSpeed);
            // return new Vector2(speedRatio, angle == 0 ? 0 : (angle > 0 ? 1 : -1));
            return new Vector2(speedRatio, 0);

        }
        private void FixedUpdate()
        {
            var movementDirection = Character.Brain.MovementDirection;
            if (movementDirection == default) return;
            Movement(movementDirection,
                Character.Parameters.RunSpeed,
                Character.Parameters.Acceleration,
                true
            );
        }
    }
}