using System;
using Animancer.Units;
using UnityEngine;

namespace FallGuy.Character
{
    [Serializable]
    public sealed class CharacterParameters
    {
        [SerializeField, Min(0)] private float _jumpForce = 15f;
        public float JumpForce => _jumpForce;
        [SerializeField, Seconds, Min(0)] private float _jumpDelay = 0.25f;
        public float JumpDelay => _jumpDelay;
        [SerializeField, Multiplier, Min(0)] private float _gravityMutiplier = 2f;
        public float GravityMutipler => _gravityMutiplier;
        [SerializeField, MetersPerSecond, Min(0)] private float _runSpeed = 4f;
        public float RunSpeed => _runSpeed;

        [SerializeField, MetersPerSecondPerSecond, Min(0)]
        private float _runAcceleration = 10;

        public float RunAcceleration => _runAcceleration;

        [SerializeField, Seconds, Min(0)] private float _turnSmoothTime = 0.16f;
        public float TurnSmoothTime => _turnSmoothTime;



    }
}