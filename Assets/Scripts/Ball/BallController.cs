namespace CT6RIGPR
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.XR;

    /// <summary>
    /// The main controller for the ball
    /// </summary>
    public class BallController : MonoBehaviour
    {
        private Rigidbody _rigidBody;
        private bool _isInputActive = false;

        [SerializeField] private float _maxForce = Constants.BALL_DEFAULT_MAX_FORCE;
        [SerializeField] public float _yRotation = 0;
        [SerializeField] private bool _debugInput;
        [SerializeField] private bool _disableInput;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private float _stopDampingDuration = 1.0f;
        [SerializeField] private float _threshold = 3f;

        [SerializeField] private float _dragMin = 0.5f;
        [SerializeField] private float _dragMax = 5f;
        [SerializeField] private float _minimalVelocity = 0f;

        [SerializeField] private Vector3 _stopVelocity = Vector3.zero;

        [SerializeField] private float _gravityModifier = 3f;

        [SerializeField, Range(0.1f, 2f)] private float _accelerationFactor = 1f;

        [SerializeField] private float _currentSpeed;

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
        /// Gets the current speed of the ball.
        /// </summary>
        public float CurrentSpeed => _currentSpeed;

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
            _rigidBody = GetComponent<Rigidbody>();
            _yRotation = 0;
        }

        private void FixedUpdate()
        {
            // Apply additional gravity (if needed)
            if (!Mathf.Approximately(_gravityModifier, 1f)) // Check if the gravity modifier is not the default value
            {
                Vector3 extraGravityForce = (Physics.gravity * _gravityModifier) - Physics.gravity;
                _rigidBody.AddForce(extraGravityForce, ForceMode.Acceleration);
            }

            Vector3 movement = CalculateMovement();
            _isInputActive = movement != Vector3.zero;

            if (!_disableInput)
            {
                UpdateControllerInput();
                if (_isInputActive)
                {
                    ApplyForce(movement);
                }
                else
                {
                    ApplyDamping();
                }

                // WIP Gravity fix with falling. Don't want to adjust this for now.
                AdjustRigidbodyDrag();

                NormalizeRotation();
            }

            if (_debugInput) // Use this condition if you want the log to appear only when debug input is enabled
            {
                _currentSpeed = _rigidBody.velocity.magnitude;
                //Debug.Log($"Current Speed: {_currentSpeed}");
            }
        }

        private void AdjustRigidbodyDrag()
        {
            Vector3 velocityDirection = _rigidBody.velocity.normalized;
            Vector3 inputDirection = CalculateMovement().normalized;

            float alignment = Vector3.Dot(velocityDirection, inputDirection);
            _rigidBody.drag = Mathf.Lerp(_dragMin, _dragMax, 1 - Mathf.Abs(alignment));
        }

        /// <summary>
        /// Apply damping to the players movement.
        /// </summary>
        private void ApplyDamping()
        {
            if (_rigidBody.velocity.magnitude > _minimalVelocity)
            {
                float dampingFactor = Mathf.Clamp01(Time.fixedDeltaTime * _stopDampingDuration);
                Vector3 newVelocity = Vector3.Lerp(_rigidBody.velocity, Vector3.zero, dampingFactor);

                Vector3 newAngularVelocity = Vector3.Lerp(_rigidBody.angularVelocity, Vector3.zero, dampingFactor);

                if (newVelocity.magnitude < _threshold)
                {
                    _rigidBody.velocity = _stopVelocity;
                    _rigidBody.angularVelocity = _stopVelocity;
                }
                else
                {
                    _rigidBody.velocity = newVelocity;
                    _rigidBody.angularVelocity = newAngularVelocity;
                }
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
        private Vector3 CalculateMovement()
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

            Transform cameraTransform = Camera.main.transform;
            Vector3 forward = cameraTransform.forward;
            forward.y = 0;
            Vector3 right = cameraTransform.right;
            Vector3 adjustedMovement = (forward * moveVertical + right * moveHorizontal).normalized;

            return adjustedMovement * _maxForce;
        }

        /// <summary>
        /// Apply force to the Rigidbody.
        /// </summary>
        /// <param name="movement">The movement vector to apply force to.</param>
        private void ApplyForce(Vector3 movement)
        {
            float currentSpeed = _rigidBody.velocity.magnitude;
            float targetSpeed = movement.magnitude * _maxForce;
            float speedDifference = targetSpeed - currentSpeed;

            // Ensure acceleration factor is effectively used
            float forceMultiplier;
            if (currentSpeed > targetSpeed)
            {
                // Optionally, apply deceleration logic here
                forceMultiplier = 0;
            }
            else
            {
                // Scale force application more directly with _accelerationFactor
                float normalizedSpeedDifference = speedDifference / targetSpeed;
                forceMultiplier = _maxForce * Mathf.Clamp(normalizedSpeedDifference * _accelerationFactor, 0, 1);
            }

            _rigidBody.AddForce(movement.normalized * forceMultiplier, ForceMode.Acceleration);
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