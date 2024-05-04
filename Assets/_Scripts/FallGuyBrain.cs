using Animancer.FSM;
using FallGuy.Character.States;
using UnityEngine;

namespace FallGuy.Character
{
    public sealed class FallGuyBrain : CharacterBrain
    {
        [SerializeField] private CharacterState _locomontionState;
        [SerializeField] private CharacterState _jumpState;
        [SerializeField] private CharacterState _fallState;
        private Camera _mainCamera;
        private void Start()
        {
            _mainCamera = Camera.main;
            Character.StateMachine.TrySetDefaultState();
        }
        private void Update()
        {
            UpdateMovementDirection();
            UpdateCharacterState();
        }
        private void UpdateMovementDirection()
        {
            var input = PlayerInput.Wasd;
            if (input != default)
            {
                var cameraTransform = _mainCamera.transform;
                var forward = cameraTransform.forward;
                forward.y = 0;
                forward.Normalize();

                var right = cameraTransform.right;
                right.y = 0;
                right.Normalize();
                MovementDirection = right * input.x + forward * input.y;
            }
            else MovementDirection = default;

        }
        private void UpdateCharacterState()
        {
            if (Character.Body.OnGround)
            {
                if (MovementDirection != default) _locomontionState.TryEnterState();
                else Character.StateMachine.TrySetDefaultState();
                if (PlayerInput.Jump) _jumpState.TryEnterState();
            }
            else
            {
                _fallState.TryEnterState();
            }
        }
    }
}