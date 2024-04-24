using System;
using Animancer.FSM;
using UnityEngine;

namespace FallGuy.Character.States
{
    public abstract class CharacterState : StateBehaviour, IOwnedState<CharacterState>
    {
        [Serializable]
        public class StateMachine : StateMachine<CharacterState>.WithDefault { }

        [SerializeField] private Character _character;
        
        public Character Character
        {
            get => _character;
            set
            {
                if (_character != null && _character.StateMachine.CurrentState == this)
                    _character.DefaultState.ForceEnterState();
                _character = value;
            }
        }

        public StateMachine<CharacterState> OwnerStateMachine => _character.StateMachine;
        protected static bool WantsRun => PlayerInput.Wasd != default;
    }
}