using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Quick and dirty script ot make a component look at the camera
    /// </summary>
    public class LookAtCamera : MonoBehaviour
    {
        private Transform _cameraTransform;

        void Start()
        {
            _cameraTransform = Camera.main.transform;
        }

        void Update()
        {
            transform.LookAt(_cameraTransform);
        }
    }
}