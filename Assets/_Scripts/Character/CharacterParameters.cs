using System;
using Animancer.Units;
using UnityEngine;

namespace FallGuy.Character
{
    [Serializable]
    public sealed class CharacterParameters
    {

        [SerializeField, MetersPerSecond] private float _runSpeed = 4;
        public float RunSpeed => _runSpeed;

        [SerializeField, MetersPerSecondPerSecond]
        private float _runAcceleration = 10;

        public float RunAcceleration => _runAcceleration;

        [SerializeField, Seconds] private float _turnSmoothTime = 0.16f;
        public float TurnSmoothTime => _turnSmoothTime;

    }
}