using System;
using UnityEngine;
using Animancer;
using FallGuy.Character.States;

namespace FallGuy.Character
{
    [DefaultExecutionOrder(-10000)]
    public sealed class Character : MonoBehaviour
    {
        [SerializeField] private CharacterBody _body;
        public CharacterBody Body => _body;

        [SerializeField] private Rigidbody _rigid;
        public Rigidbody Rigidbody => _rigid;

        [SerializeField] private AnimancerComponent _animancer;
        public AnimancerComponent Animancer => _animancer;
        [SerializeField] private CharacterBrain _brain;


        public CharacterBrain Brain
        {
            get => _brain;
            set
            {
                if (_brain == value) return;

                var oldBrain = _brain;
                _brain = value;

                if (oldBrain != null) oldBrain.Character = null;

                if (value != null) value.Character = this;
            }
        }

        [SerializeField] private CharacterParameters _parameters;
        public CharacterParameters Parameters => _parameters;

        [SerializeField] private CharacterState _defaultState;
        public CharacterState DefaultState => _defaultState;

        public readonly CharacterState.StateMachine StateMachine = new();

        private void Awake()
        {
            StateMachine.DefaultState = _defaultState;
        }
    }
}