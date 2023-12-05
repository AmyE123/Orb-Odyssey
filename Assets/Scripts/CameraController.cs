namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// The camera controller places the XR origin at the position of the ball.
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
            Vector3 targetPosition = _playerGameObject.transform.position + _offset;
            targetPosition.y = targetPosition.y - Constants.CAMERA_Y_OFFSET;
            transform.position = targetPosition;
        }
    }
}