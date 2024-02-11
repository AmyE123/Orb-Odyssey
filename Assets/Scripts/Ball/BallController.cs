namespace CT6RIGPR
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.XR;

    /// <summary>
    /// The main controller for the ball
    /// </summary>
    public class BallController : MonoBehaviour
    {
        private Vector3 _lastPosition;

        [SerializeField] private float _maxForce = Constants.BALL_DEFAULT_MAX_FORCE;
        [SerializeField] public float _yRotation = 0;
        [SerializeField] private bool _debugInput;
        [SerializeField] private bool _disableInput;
        [SerializeField] private GameManager _gameManager;

        /// <summary>
        /// The Y rotation of the ball.
        /// </summary>
        public float YRotation => _yRotation;

        /// <summary>
        /// A getter for whether we have debug input enabled.
        /// </summary>
        public bool DebugInput => _debugInput;

        /// <summary>
        /// Whether the input for the ball has been disabled or not.
        /// </summary>
        public bool HasDisabledInput => _disableInput;        

        /// <summary>
        /// Disables the players input.
        /// </summary>
        public void DisableInput()
        {
            _disableInput = true;
        }

        /// <summary>
        /// Enables the players input.
        /// </summary>
        public void EnableInput()
        {
            _disableInput = false;
        }
        private void Start()
        {
            _yRotation = 0;
        }

        private void FixedUpdate()
        {
            Vector3 movement = CalculateMovement();

            if (!_disableInput)
            {
                UpdateControllerInput();                
                ApplyForce(movement);
                NormalizeRotation();
            }
        }

        /// <summary>
        /// Handle controller input and update _yRotation.
        /// Currently copied from the Actuate example (?)
        /// </summary>
        private void UpdateControllerInput()
        {
            if (_debugInput && !_gameManager.GlobalReferences.CameraController.DebugMouseLook)
            {
                if (Input.GetKey(KeyCode.RightControl))
                {
                    _yRotation--;
                }

                if (Input.GetKey(KeyCode.RightShift))
                {
                    _yRotation++;
                }
            }
            else
            {
                var leftHandedControllers = new List<InputDevice>();
                var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
                InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

                foreach (var device in leftHandedControllers)
                {
                    Vector2 thumbstick;
                    if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out thumbstick))
                    {
                        _yRotation += thumbstick.x;
                    }
                }
            }
        }

        /// <summary>
        /// Calculate and return the movement vector.
        /// </summary>
        /// <returns>The movement vector</returns>
        public Vector3 CalculateMovement()
        {
            // Calculate and return the movement vector
            float moveHorizontal = 0.0f;
            float moveVertical = 0.0f;

            if (!_disableInput)
            {
                if (_debugInput)
                {
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                    {
                        moveHorizontal--;
                    }

                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                    {
                        moveHorizontal++;
                    }

                    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                    {
                        moveVertical++;
                    }

                    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                    {
                        moveVertical--;
                    }
                }
                else
                {
                    moveHorizontal = Input.GetAxis(Constants.HOTAS_X);
                    moveVertical = Input.GetAxis(Constants.HOTAS_Y);

                }

                if (!_gameManager.GlobalReferences.CameraController.DebugMouseLook)
                {
                    Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
                    movement = Quaternion.AngleAxis(_yRotation, Vector3.up) * movement;
                    return movement;
                }
            }

            // Assuming the camera's forward direction dictates movement direction
            Transform cameraTransform = Camera.main.transform;
            // Remove any y-component to keep movement horizontal
            Vector3 forward = cameraTransform.forward;
            forward.y = 0;
            Vector3 right = cameraTransform.right;

            // Calculate movement direction relative to the camera's orientation
            Vector3 adjustedMovement = (forward * moveVertical + right * moveHorizontal).normalized;

            return adjustedMovement * _maxForce; // Apply any scaling factor as needed
        }

        /// <summary>
        /// Apply force to the Rigidbody.
        /// Currently copied from the Actuate example
        /// </summary>
        /// <param name="movement">The movement vector to apply force to.</param>
        public void ApplyForce(Vector3 movement)
        {
            // Apply force to the Rigidbody
            Vector3 position = gameObject.transform.position;

            // TODO: Update these values to not include magic numbers (consts plz!)
            float speed = Mathf.Clamp((_lastPosition - position).magnitude * 2.0f, 0, 1);
            float force = (_maxForce * -(speed - 1));
            GetComponent<Rigidbody>().AddForce(movement * force * Time.deltaTime);

            _lastPosition = position;
        }

        public void MoveBall(Vector3 targetPosition)
        {
            DOTween.Kill(gameObject.transform);

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                rb.MovePosition(targetPosition);
            }
            else
            {
                transform.position = targetPosition;
            }
        }


        /// <summary>
        /// Normalize _yRotation to be within -180 to 180 degrees
        /// </summary>
        private void NormalizeRotation()
        {
            if (_yRotation < -180) { _yRotation += 360; }
            if (_yRotation > 180) { _yRotation -= 360; }
        }
    }
}