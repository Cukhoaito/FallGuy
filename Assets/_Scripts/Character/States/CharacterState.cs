using System;
using Animancer.FSM;
using UnityEngine;

namespace FallGuy.Character.States
{
    public abstract class CharacterState :
        StateBehaviour,
        IOwnedState<CharacterState>
    {
        [Serializable]
        public class StateMachine : StateMachine<CharacterState>.WithDefault { }
        [SerializeField] private Character _character;
        private float _currentTurnVelocity;

        public Character Character
        {
            get => _character;
            set
            {
                if (_character != null && _character.StateMachine.CurrentState == this)
                    _character.DefaultState.ForceEnterState();
                _character = value;
            }
        }

        public StateMachine<CharacterState> OwnerStateMachine
            => _character.StateMachine;


        protected virtual void ExtraGravity(float gravityMutipler)
        {
            if (gravityMutipler == 0f) return;
            var rigid = Character.Rigidbody;
            if (rigid.velocity.y < 0)
            {
                rigid.velocity += gravityMutipler
                    * Physics.gravity.y * Time.fixedDeltaTime * Vector3.up;
            }
        }
        protected virtual void Movement(Vector3 direction, float speed, float acceleration)
        {
            if (direction == default) return;
            var targetVelocity = direction * speed;
            var currentVelocity = Character.Rigidbody.velocity;
            var accelerationVector = (targetVelocity - currentVelocity) * acceleration;

            Character.Rigidbody.AddForce(accelerationVector);
        }
        protected virtual void Turning(Vector3 direction)
        {
            if (direction == default) return;
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var trans = Character.Body.Transform;
            var angle = Mathf.SmoothDampAngle(trans.eulerAngles.y, targetAngle, ref _currentTurnVelocity,
                Character.Parameters.TurnSmoothTime);
            trans.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }
    }
}