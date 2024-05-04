using System;
using Animancer.Units;
using UnityEngine;

namespace FallGuy.Character
{
    [Serializable]
    public sealed class CharacterParameters
    {
        [Header("Locomotion Settings")]
        [SerializeField, Min(0)] private float _runSpeed = 8f;
        public float RunSpeed => _runSpeed;
        [SerializeField, Min(0)]
        private float _acceleration = 4;
        public float Acceleration => _acceleration;

        [SerializeField, Seconds, Min(0)] private float _turnSmoothTime = 0.16f;
        public float TurnSmoothTime => _turnSmoothTime;
        [Header("Jump Settings")]
        [SerializeField] float _jumpHeight = 2f;
        public float JumpHeight => _jumpHeight;
        [SerializeField, Seconds, Min(0)] private float _jumpDelay = 0.25f;
        public float JumpDelay => _jumpDelay;
        [SerializeField, Multiplier, Min(0)] private float _gravityMutiplier = 2f;
        public float GravityMutiplier => _gravityMutiplier;
    }
}