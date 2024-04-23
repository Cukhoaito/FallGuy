using UnityEngine;

namespace FallGuy
{
    public static class Vector3Helper
    {
        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.z);
        }
    }
}