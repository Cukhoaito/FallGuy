using Animancer;
using UnityEngine;

namespace FallGuy.Character.States
{
    public class JumpState : CharacterState
    {
        [SerializeField] private ClipTransition _startAnimation;
        [SerializeField] private ClipTransition _loopAnimation;

        public override bool CanExitState => Character.IsGrounded == true;

        private void OnEnable()
        {
            Character.Rigidbody.velocity = Vector3.up * 10f;
            PlayStartAnimation();
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