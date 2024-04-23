using Animancer;
using UnityEngine;

namespace FallGuy.Character.States
{
    public class IdleState : CharacterState
    {
        [SerializeField] private ClipTransition _animation;

        private void OnEnable()
        {
            Character.Animancer.Play(_animation);
        }

        private void FixedUpdate()
        {
            Character.Rigidbody.velocity = default;
        }
    }
}