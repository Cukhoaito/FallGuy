using UnityEngine;

namespace FallGuy.Character
{
    public abstract class CharacterBrain : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Vector3 _movementDirection;
        public Character Character
        {
            get => _character;
            set
            {
                if (_character == value) return;
                var oldCharacter = _character;
                _character = value;

                if (oldCharacter != null) oldCharacter.Brain = null;

                if (value != null)
                {
                    value.Brain = this;
                    enabled = true;
                }
                else enabled = false;
            }
        }
        public Vector3 MovementDirection
        {
            get
            {
                _movementDirection.y = 0;
                return Vector3.ClampMagnitude(_movementDirection, 1);
            }
            protected set => _movementDirection = value;
        }
    }
}