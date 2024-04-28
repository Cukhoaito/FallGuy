using Animancer;
using UnityEngine;

namespace FallGuy.Character.States
{
    public class IdleState : CharacterState
    {
        [SerializeField] private ClipTransition _animation;

        private void OnEnable()
        {
            Character.Rigidbody.velocity = default;
            Character.Animancer.Play(_animation);
        }
    }
}