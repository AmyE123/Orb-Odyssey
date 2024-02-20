namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// The camera controller places the XR origin at the position of the ball.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        private Vector3 _offset;
        private float _xRotation;

        [SerializeField] private GlobalGameReferences _references;
        [SerializeField] private float _mouseSensitivity;
        [SerializeField] private GameObject _cockpit;
        [SerializeField] private GameObject _outerBall;
        [SerializeField] private GameObject _cameraObj;
        [SerializeField] private bool _debugMouseLook = true;

        public bool DebugMouseLook => _debugMouseLook;


        void Start()
        {
            _offset = transform.position;

            if (_debugMouseLook)
            {
                _offset = transform.position - _cockpit.transform.position;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        void LateUpdate()
        {
            UpdateCameraPosition();
        }

        private void Update()
        {
            if (_debugMouseLook && !_references.BallController.DisableRotation)
            {
                MouseLookAround();
            }

            if (_references.BallController.DisableRotation)
            {
                _cockpit.transform.localRotation = Quaternion.Euler(Vector3.zero);
                _outerBall.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
        }

        private void UpdateCameraPosition()
        {
            Vector3 targetPosition = _cockpit.transform.position + _offset;        

            if (_debugMouseLook)
            {
                targetPosition.y = Mathf.Max(targetPosition.y - Constants.CAMERA_Y_OFFSET, _cockpit.transform.position.y);
            }
            else
            {
                targetPosition.y = targetPosition.y - Constants.CAMERA_Y_OFFSET;
            }
            
            transform.position = targetPosition;
        }

        private void MouseLookAround()
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f); // Clamping the vertical rotation

            // Apply the vertical rotation to the camera
            _cameraObj.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

            // Apply the horizontal rotation to the player object or the camera itself
            // If you want the camera and player body to rotate together, use the player object's transform
            _cockpit.transform.Rotate(Vector3.up * mouseX);
            _outerBall.transform.Rotate(Vector3.up * mouseX);

            // If you want the camera to rotate independently from the player body, adjust the camera's rotation directly
            transform.Rotate(Vector3.up * mouseX);
            
        }
    }
}