using UnityEngine;

namespace FallGuy
{
    public static class PlayerInput
    {
        public static Vector2 Wasd
            => new (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        public static bool Jump => Input.GetKey(KeyCode.Space);
        
    }
}