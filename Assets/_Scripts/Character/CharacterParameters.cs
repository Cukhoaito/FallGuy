using System;
using Animancer.Units;
using UnityEngine;

namespace FallGuy.Character
{
    [Serializable]
    public sealed class CharacterParameters
    {
        [Header("Locomotion")]
        [SerializeField, Min(0)] private float _maxSpeed = 8f;
        public float MaxSpeed => _maxSpeed;

        [SerializeField, Min(0)]
        private float _acceleration = 4;
        public float Acceleration => _acceleration;

        [SerializeField, Multiplier, Min(0)] private float _gravityScale = 2f;
        public float GravityScale => _gravityScale;

        [SerializeField, Seconds, Min(0)] private float _turnSmoothTime = 0.16f;
        public float TurnSmoothTime => _turnSmoothTime;
        [Header("Jump")]
        [SerializeField, Min(0)] private float _jumpForce = 15f;
        public float JumpForce => _jumpForce;
        [SerializeField, Seconds, Min(0)] private float _jumpDelay = 0.25f;
        public float JumpDelay => _jumpDelay;


    }
}