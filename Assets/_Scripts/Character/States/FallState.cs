
using Animancer;
using UnityEngine;

namespace FallGuy.Character.States
{
    public class FallState : CharacterState
    {
        [SerializeField] private ClipTransition _animation;
        public override bool CanExitState => Character.Body.IsGrounded;

        private void OnEnable()
        {
            Character.Animancer.Play(_animation);
        }
        private void Update()
        {
            AddExtraGravity();
        }
    }
}