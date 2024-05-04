using UnityEngine;

namespace FallGuy.Utilities
{
    [ExecuteAlways]
    public sealed class OrbitControls : MonoBehaviour
    {
        [SerializeField] private Transform _follow;
        [SerializeField] private Vector3 _focalPoint;
        [SerializeField] private MouseButton _mouseButton = MouseButton.Right;
        [SerializeField] private Vector3 _sensitivity = new Vector3(15, -10, -0.1f);

        private float _distance;


        private enum MouseButton
        {
            Automatic = -1,
            Left = 0,
            Right = 1,
            Middle = 2,
        }


        private void Awake()
        {
            _distance = Vector3.Distance(_focalPoint, transform.position);
            _focalPoint = _follow == null ? new Vector3(0, 1, 0) : _follow.position;
            transform.LookAt(_focalPoint);
        }


        private void Update()
        {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                transform.LookAt(_focalPoint);
                return;
            }
#endif

            if (_mouseButton == MouseButton.Automatic || Input.GetMouseButton((int)_mouseButton))
            {
                var movement = new Vector2(
                    Input.GetAxis("Mouse X"),
                    Input.GetAxis("Mouse Y"));

                if (movement != default)
                {
                    var euler = transform.localEulerAngles;
                    euler.y += movement.x * _sensitivity.x;
                    euler.x += movement.y * _sensitivity.y;
                    if (euler.x > 180)
                        euler.x -= 360;
                    euler.x = Mathf.Clamp(euler.x, -80, 80);
                    transform.localEulerAngles = euler;
                }
            }

            var zoom = Input.mouseScrollDelta.y * _sensitivity.z;
            if (zoom != 0 &&
                Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
                Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height)
            {
                _distance *= 1 + zoom;
            }

            UpdatePosition();
        }


        private void UpdatePosition()
        {
            _focalPoint = _follow == null ? new Vector3(0, 1, 0) : _follow.position;
            transform.position = _focalPoint - transform.forward * _distance;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0.5f, 1, 0.5f, 1);
            Gizmos.DrawLine(transform.position, _focalPoint);
        }

    }
}
