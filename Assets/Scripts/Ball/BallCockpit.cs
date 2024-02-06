namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// Manages the cockpit's position, rotation and other values
    /// </summary>
    public class BallCockpit : MonoBehaviour
    {
        private float _roll;
        private float _pitch;

        [SerializeField] private GameObject _playerGameObject;

        /// <summary>
        /// The roll value for the cockpit
        /// </summary>
        public float Roll => _roll;

        /// <summary>
        /// The roll value for the cockpit
        /// </summary>
        public float Pitch => _pitch;

        /// <summary>
        /// Sets the roll value for the cockpit
        /// </summary>
        /// <param name="value">The value to set the roll to</param>
        public void SetRoll(float value)
        {
            _roll = value;
        }

        /// <summary>
        /// Sets the pitch value for the cockpit
        /// </summary>
        /// <param name="value">The value to set the pitch to</param>
        public void SetPitch(float value)
        {
            _pitch = value;
        }

        void FixedUpdate()
        {
            UpdatePosition();
            if (!_playerGameObject.GetComponent<BallController>().HasDisabledInput)
            {
                CalculateCockpitRotation();
            }
            ClampRotation();
            ApplyRotation();
            DrawDebugRay();
        }

        /// <summary>
        /// Sets cockpit GameObject position to be the same as the ball.
        /// </summary>
        private void UpdatePosition()
        {
            transform.position = _playerGameObject.transform.position;
        }

        /// <summary>
        /// Calculate the cockpit's rotation.
        /// </summary>
        private void CalculateCockpitRotation()
        {
            float moveHorizontal = Input.GetAxis("HotasX");
            float moveVertical = Input.GetAxis("HotasY");

            //Use arrow keys for input if testing with the PC rather than joysticks.
            if (_playerGameObject.GetComponent<BallController>().DebugInput)
            {
                if (Input.GetKey(KeyCode.LeftArrow)) { _roll--; }
                if (Input.GetKey(KeyCode.RightArrow)) { _roll++; }
                if (Input.GetKey(KeyCode.UpArrow)) { _pitch++; }
                if (Input.GetKey(KeyCode.DownArrow)) { _pitch--; }
            }
            else
            {
                _roll += moveHorizontal / 2.0f;
                _pitch += moveVertical / 2.0f;               
            }

            //Lerp to 0,Y,0. Doing this before clamping will result in it not being noticeable when actively pushing the joystick.
            _roll = Mathf.Lerp(_roll, 0, Time.deltaTime);
            _pitch = Mathf.Lerp(_pitch, 0, Time.deltaTime);
            
        }

        /// <summary>
        /// Normalise _roll to be within -16.8 to 16.8 degrees.
        /// Normalise _pitch to be within -19.8 to 19.2 degrees. 
        /// </summary>
        private void ClampRotation()
        {
            _roll = Mathf.Clamp(_roll, -Constants.ROLL_CLAMP, Constants.ROLL_CLAMP);
            _pitch = Mathf.Clamp(_pitch, Constants.PITCH_CLAMP_NEG, Constants.PITCH_CLAMP_POS);
        }

        /// <summary>
        /// Apply the rotation to the transform.
        /// </summary>
        private void ApplyRotation()
        {
            Quaternion yawRotation = Quaternion.AngleAxis(_playerGameObject.GetComponent<BallController>().YRotation, Vector3.up);
            Quaternion rollRotation = Quaternion.AngleAxis(-_roll, Vector3.forward);
            Quaternion pitchRotation = Quaternion.AngleAxis(-_pitch, Vector3.left);

            transform.rotation = yawRotation * pitchRotation * rollRotation;          
        }

        private void DrawDebugRay()
        {
            Debug.DrawRay(transform.position, transform.forward * 100, Color.blue);
        }
    }
}