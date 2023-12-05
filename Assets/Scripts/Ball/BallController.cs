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
        [SerializeField] private float _maxForce = Constants.BALL_DEFAULT_MAX_FORCE;
        [SerializeField] private float _yRotation = 0;
        [SerializeField] private bool _debugInput;

		public float YRotation => _yRotation;
        public bool DebugInput => _debugInput;

        private Vector3 _lastPosition;

        void Start()
        {
            _yRotation = 0;
        }

        private void FixedUpdate()
        {
            UpdateControllerInput();
            Vector3 movement = CalculateMovement();
            ApplyForce(movement);
            NormalizeRotation();
        }

        /// <summary>
        /// Handle controller input and update _yRotation.
        /// Currently copied from the Actuate example (?)
        /// </summary>
        private void UpdateControllerInput()
        {
            if (_debugInput)
            {
                if (Input.GetKey(KeyCode.RightControl)) { _yRotation--; }
                if (Input.GetKey(KeyCode.RightShift)) { _yRotation++; }
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
            if (_debugInput)
            {
                if (Input.GetKey(KeyCode.LeftArrow)) { moveHorizontal--; }
                if (Input.GetKey(KeyCode.RightArrow)) { moveHorizontal++; }
                if (Input.GetKey(KeyCode.UpArrow)) { moveVertical++; }
                if (Input.GetKey(KeyCode.DownArrow)) { moveVertical--; }
            }
            else
            {
                moveHorizontal = Input.GetAxis("HotasX");
                moveVertical = Input.GetAxis("HotasY");

            }
            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
            movement = Quaternion.AngleAxis(_yRotation, Vector3.up) * movement;
            return movement;
        }

        /// <summary>
        /// Apply force to the Rigidbody.
        /// Currently copied from the Actuate example (?)
        /// </summary>
        /// <param name="movement">The movement vector to apply force to.</param>
        private void ApplyForce(Vector3 movement)
        {
            // Apply force to the Rigidbody
            Vector3 position = gameObject.transform.position;

            // TODO: Update these values to not include magic numbers (consts plz!)
            float speed = Mathf.Clamp((_lastPosition - position).magnitude * 2.0f, 0, 1);
            float force = (_maxForce * -(speed - 1));
            GetComponent<Rigidbody>().AddForce(movement * force * Time.deltaTime);

            _lastPosition = position;
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