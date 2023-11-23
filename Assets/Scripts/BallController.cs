namespace CT6RIGPR
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.XR;

    public class BallController : MonoBehaviour
    {
        // TODO: Make a const file to access this magic number
        [SerializeField] private float _maxForce = 500;
        [SerializeField] private float _yRotation = 0;

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
            var leftHandedControllers = new List<InputDevice>();
            var desiredCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
            InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

            foreach (var device in leftHandedControllers)
            {
                Vector2 thumbstick;
                if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out thumbstick))
                {
                    Debug.Log(thumbstick.x);
                    _yRotation += thumbstick.x;
                }
            }
        }

        /// <summary>
        /// Calculate and return the movement vector.
        /// Currently copied from the Actuate example (?)
        /// </summary>
        /// <returns>The movement vector</returns>
        private Vector3 CalculateMovement()
        {
            // Calculate and return the movement vector
            float moveHorizontal = Input.GetAxis("HotasX");
            float moveVertical = Input.GetAxis("HotasY");
            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

            //TODO: Need to store rotation and transform movement by that rotation.
            //Easier alternative: rebuild this. Figure out the facing vector 3 from the rotation. Create the vectors for forward and horizontal movement then combine them.

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