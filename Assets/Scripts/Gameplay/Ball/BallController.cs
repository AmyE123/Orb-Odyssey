using CT6RIGPR;

namespace CT6RIGPR
{
    using DG.Tweening;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
//    using UnityEngine.InputSystem;
    using UnityEngine.XR;

    /// <summary>
    /// The main controller for the ball
    /// </summary>
    public class BallController : MonoBehaviour
    {
        private bool _isInputActive = false;
        private float moveHorizontal = 0.0f;
        private float moveVertical = 0.0f;

        [Header("Movement Settings")]
        [SerializeField] private float _maxForce = Constants.BALL_DEFAULT_MAX_FORCE;
        [SerializeField] private float _yRotation = 0;
        [SerializeField] private float _stopDampingDuration = 1.0f;
        [SerializeField] private float _threshold = 3f;
        [SerializeField, Range(0.1f, 2f)] private float _accelerationFactor = 1f;
        [SerializeField] private Rigidbody _rigidBody;

        [Header("Drag Settings")]
        [SerializeField] private float _dragMin = 0.5f;
        [SerializeField] private float _dragMax = 5f;

        [Header("Gravity Settings")]
        [SerializeField] private float _gravityModifier = 3f;
        public Vector3 _ballGravity = Physics.gravity;
        [SerializeField] private float _additionalAirborneGravity = 2f;
        [SerializeField] private float _airborneDrag = 0.1f;

        [Header("Velocity Settings")]
        [SerializeField] private float _minimalVelocity = 0f;
        [SerializeField] private Vector3 _stopVelocity = Vector3.zero;

        [Header("Debug and Input Settings")]
        [SerializeField] private bool _debugInput;
        [SerializeField] private bool _joystickWorking;
        [SerializeField] private bool _disableInput;
        [SerializeField] private bool _disableRotation;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameObject _noseObj;

        [Header("Ground Check Settings")]
        [SerializeField] private bool _grounded;
        [SerializeField] private float _groundCheckDistance = 1f;

        [Header("Runtime Values")]
        [SerializeField] private float _currentSpeed;
        [SerializeField] private bool _canStartPlaying = false;

        [Header("Visual Values")]
        [SerializeField] private Material _ballOuterMat;

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
        /// Whether the ball is grounded or not.
        /// </summary>
        public bool Grounded => _grounded;

        /// <summary>
        /// The max force of the ball.
        /// </summary>
        public float MaxForce => _maxForce;

        /// <summary>
        /// Whether rotation of the ball is disabled or not.
        /// </summary>
        public bool DisableRotation => _disableRotation;


        public float MoveHorizontal => moveHorizontal;
        public float MoveVertical => moveVertical;


        /// <summary>
        /// This toggles the debug input.
        /// </summary>
        public void ToggleDebugInput()
        {
            _debugInput = !_debugInput;
            _noseObj.SetActive(!_debugInput);
        }

        /// <summary>
        /// Disables the players input.
        /// </summary>
        public void DisableInput(bool disableRotation = false)
        {
            _disableInput = true;
            _disableRotation = disableRotation;
        }

        /// <summary>
        /// Enables the players input.
        /// </summary>
        public void EnableInput()
        {
            _disableInput = false;
            _disableRotation = false;
        }

        /// <summary>
        /// Freeze the players rigidbody.
        /// </summary>
        public void FreezePlayer()
        {
            if (_rigidBody != null)
            {
                _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        /// <summary>
        /// Unfreezes the players rigidbody.
        /// </summary>
        public void UnfreezePlayer()
        {
            if (_rigidBody != null)
            {
                _rigidBody.constraints = RigidbodyConstraints.None;
                _rigidBody.constraints = RigidbodyConstraints.FreezeRotationZ;
            }
        }

        /// <summary>
        /// Fades the outer material of the ball from clear to black.
        /// </summary>
        public Tween FadeOutBall()
        {
            if (_ballOuterMat != null)
            {
                FreezePlayer();
                return _ballOuterMat.DOColor(Color.black, 1).SetDelay(1); // Directly return the Tween
            }
            else
            {
                Debug.LogWarning("[CT6RIGPR]: Material to fade is not assigned.");
                return null;
            }
        }

        /// <summary>
        /// Fades the outer material of the ball from black to clear.
        /// </summary>
        public Tween FadeInBall()
        {
            if (_ballOuterMat != null)
            {
                FreezePlayer();
                return _ballOuterMat.DOColor(Color.clear, 1).SetDelay(1); // Directly return the Tween
            }
            else
            {
                Debug.LogWarning("[CT6RIGPR]: Material to fade is not assigned.");
                return null;
            }
        }

        /// <summary>
        /// A function to change the max force of the ball.
        /// </summary>
        /// <param name="newMaxForce">The new value of the max force.</param>
        public void ChangeMaxForce(float newMaxForce)
        {
            if (newMaxForce != _maxForce)
            {
                _maxForce = newMaxForce;
            }
        }

        private void Start()
        {
            ApplyInitialPlayerFreeze();
            _rigidBody = GetComponent<Rigidbody>();
            _yRotation = 0;
        }

        private void ApplyInitialPlayerFreeze()
        {
            Material ballMat = _gameManager.GlobalGameReferences.BallMaterial;
            _canStartPlaying = false;

            if (ballMat != null)
            {
                ballMat.color = Color.black;
                FreezePlayer();            
            }
            else
            {
                Debug.LogWarning("[CT6RIGPR]: Material to fade is not assigned.");
            }
        }

        private void FadeIntoGame()
        {
            Material ballMat = _gameManager.GlobalGameReferences.BallMaterial;
            _canStartPlaying = true;

            if (ballMat != null)
            {
                ballMat.DOColor(Color.clear, 1).SetDelay(1)
                .OnComplete(() =>
                {
                    Cursor.lockState = CursorLockMode.None;
                    UnfreezePlayer();
                });

            }
            else
            {
                Debug.LogWarning("[CT6RIGPR]: Material to fade is not assigned.");
            }
        }

        private bool IsGrounded()
        {
            Debug.DrawLine(transform.position, -Vector3.up * _groundCheckDistance, Color.red);
            return Physics.Raycast(transform.position, -Vector3.up, _groundCheckDistance);
        }

        private void Update()
        {
            if (_gameManager.GlobalGameReferences.LevelManager.HasReadWarning && !_canStartPlaying)
            {
                FadeIntoGame();
            }
        }

        private void FixedUpdate()
        {
            _grounded = IsGrounded();

            // Apply normal gravity when grounded or increased gravity when in the air
            Vector3 gravityForce = _ballGravity * (_gravityModifier + (_grounded ? 0 : _additionalAirborneGravity));
            _rigidBody.AddForce(gravityForce, ForceMode.Acceleration);

            Vector3 movement = CalculateMovement();
            _isInputActive = movement != Vector3.zero;

            if (!_disableInput)
            {
                UpdateControllerInput();
                if (_isInputActive)
                {
                    ApplyForce(movement);
                }
                else if (!_grounded)
                {
                    // Optionally adjust drag here for airborne state
                    _rigidBody.drag = _airborneDrag; // Set _airborneDrag to a desired value
                }
                else
                {
                    ApplyDamping();
                }

                AdjustRigidbodyDrag();

                NormalizeRotation();
            }

            if (_debugInput)
            {
                _currentSpeed = _rigidBody.velocity.magnitude;
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
            if (_gameManager.GlobalGameReferences.PowerupManager.IsSticking)
            {
                return;
            }

            if (_debugInput && !_gameManager.GlobalGameReferences.CameraController.DebugMouseLook)
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
                if (_joystickWorking)
                {
                    _yRotation += Input.GetAxis(Constants.HOTAS_X) * Constants.ROTATION_MULTIPLIER;
                }
                else
                {

                    Vector2 joystick = Vector2.zero;
                    var rightHandedControllers = new List<InputDevice>();
                    var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
                    InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

                    foreach (var device in rightHandedControllers)
                    {
                        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick);
                    }
                    if (Mathf.Abs(joystick.x) > 0.4f)
                    {
                        _yRotation += joystick.x * Constants.ROTATION_MULTIPLIER;
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
            moveHorizontal = 0.0f;
            moveVertical = 0.0f;
            float moveAltitude = 0.0f;

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
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        moveAltitude++;
                    }
                    if (Input.GetKey(KeyCode.LeftControl))
                    {
                        moveAltitude--;
                    }
                }
                else
                {
                    //Left hand input
                    Vector2 joystick = Vector2.zero;
                    var leftHandedControllers = new List<InputDevice>();
                    var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
                    InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

                    foreach (var device in leftHandedControllers)
                    {
                        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick);
                    }

                    if(_gameManager.GlobalGameReferences.PowerupManager.IsSticking)
                    {
                        moveAltitude += joystick.y;
                    }
                    else
                    {
                        moveVertical += joystick.y;
                        moveHorizontal += joystick.x;
                    }

                    //Right hand input
                    if (_joystickWorking)
                    {
                        moveVertical += Input.GetAxis(Constants.HOTAS_Y);
                    }
                    else
                    {
                        joystick = Vector2.zero;
                        var rightHandedControllers = new List<InputDevice>();
                        desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
                        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, rightHandedControllers);

                        foreach (var device in rightHandedControllers)
                        {
                            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystick);
                        }
                        if (Mathf.Abs(joystick.y) > 0.4f)
                        {
                            moveVertical += joystick.y;
                        }
                    }
                }

                if (!_gameManager.GlobalGameReferences.CameraController.DebugMouseLook)
                {
                    Vector3 movement = new Vector3(moveHorizontal, moveAltitude, moveVertical);
                    movement = Quaternion.AngleAxis(_yRotation, Vector3.up) * movement;
                    return movement;
                }
            }

            if (!_gameManager.HasCompletedLevel)
            {
                Transform cameraTransform = Camera.main.transform;
                Vector3 forward = cameraTransform.forward;
                forward.y = 0;
                Vector3 right = cameraTransform.right;
                Vector3 adjustedMovement = (forward * moveVertical + Vector3.up * moveAltitude + right * moveHorizontal).normalized;

                return adjustedMovement * _maxForce;
            }
            else
            {
                return Vector3.zero;
            }

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
        /// Prevent _yRotation from exceeding the limits of the 4-DoF.
        /// </summary>
        private void NormalizeRotation()
        {
            if (_yRotation < -720) { _yRotation = -720; }
            if (_yRotation > 720) { _yRotation = 720; }
        }
    }
}