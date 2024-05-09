using System;
using Animancer.Units;
using UnityEngine;

namespace FallGuy.Character
{
    [Serializable]
    public sealed class CharacterParameters
    {
        [Header("Locomotion Settings")]
        [SerializeField, Min(0)] private float _moveSpeed = 8f;
        public float MoveSpeed => _moveSpeed;

        [SerializeField, Min(0)] private float _airMoveSpeed = 5f;
        public float AirMoveSpeed => _airMoveSpeed;

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