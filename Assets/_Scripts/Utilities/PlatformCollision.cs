using UnityEngine;
namespace FallGuy.Utilities
{
    public class PlatformCollision : MonoBehaviour
    {
        [SerializeField]
        private string _tagName = "Moving Platform";
        private Transform _platform;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag(_tagName)) return;
            var contact = other.GetContact(0);

            if (contact.normal.y < 0.5f) return;
            _platform = other.transform;
            transform.SetParent(_platform);
        }
        private void OnCollisionExit(Collision other)
        {
            if (!other.gameObject.CompareTag(_tagName)) return;
            transform.SetParent(null);
            _platform = null;
        }
        
    }

}
