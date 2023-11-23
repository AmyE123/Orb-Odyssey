namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// Manages the cockpit's position, rotation and other values
    /// </summary>
    public class BallCockpit : MonoBehaviour
    {
		[SerializeField] private GameObject _playerGameObject;
		[SerializeField] private bool _debugInput;
		//private Vector3 _lastBallEulers;
		//private Vector3 _cockpitEulers;
		//private Vector3 _ballEulers;
		private float _roll;
        private float _pitch;

        void FixedUpdate()
        {
            UpdatePosition();
            CalculateCockpitRotation();
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

        private void CalculateCockpitRotation()
        {
            float moveHorizontal = Input.GetAxis("HotasX");
            float moveVertical = Input.GetAxis("HotasY");

            //Use arrow keys for input if testing with the PC rather than joysticks.
            if (_debugInput)
            {
				if (Input.GetKey(KeyCode.LeftArrow)) { _roll--; }
				if (Input.GetKey(KeyCode.RightArrow)) { _roll++; }
				if (Input.GetKey(KeyCode.UpArrow)) { _pitch++; }
				if (Input.GetKey(KeyCode.DownArrow)) { _pitch--; }
			}
            else
            {
				_roll += moveHorizontal;
				_pitch += moveVertical;
			}

			//Lerp to 0,Y,0. Doing this before clamping will result in it not being noticeable when actively pushing the joystick.
			_roll = Mathf.Lerp(_roll, 0, Time.deltaTime);
			_pitch = Mathf.Lerp(_pitch, 0, Time.deltaTime);
        }

        private void ClampRotation()
        {
            _roll = Mathf.Clamp(_roll, -16.8f, 16.8f);
            _pitch = Mathf.Clamp(_pitch, -19.8f, 19.2f);
        }

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