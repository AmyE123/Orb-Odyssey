namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// The camera controller sets values for the camera
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject _playerGameObject;
        
        private Vector3 _offset;

        void Start()
        {
            _offset = transform.position;
        }

        void LateUpdate()
        {
            UpdateCameraPosition();
        }

        private void UpdateCameraPosition()
        {
            transform.position = _playerGameObject.transform.position + _offset;
        }
    }
}