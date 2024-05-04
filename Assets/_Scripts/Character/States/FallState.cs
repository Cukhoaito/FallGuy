using Animancer;
using UnityEngine;

namespace FallGuy.Character.States
{
    public class FallState : CharacterState
    {
        [SerializeField] private ClipTransition _animation;
        public override bool CanExitState => Character.Body.OnGround;
        private void OnEnable()
        {
            Character.Rigidbody.velocity = default;
            Character.Animancer.Play(_animation);
        }
        private void FixedUpdate()
        {
            ExtraGravity(Character.Parameters.GravityMutiplier);
        }
    }
}