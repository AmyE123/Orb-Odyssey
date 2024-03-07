namespace CT6RIGPR
{
    using UnityEngine;
    using Actuate;

    /// <summary>
    /// Manages the motion source for the ball through the actuate agent
    /// </summary>
    public class BallMotionSource : MonoBehaviour
    {
        private float _roll;
        private float _pitch;
        private float _yaw;
        private float _altitude;

        [SerializeField] private ActuateAgent _actuateAgent;
        [SerializeField] private BallCockpit _cockpit;
        [SerializeField] private BallController _controller;


        void Start()
        {
            _actuateAgent.SetMotionSource(this.gameObject);
        }

        private void LateUpdate()
        {
            getTransformValues();
            setTransformValues();
        }

        private void getTransformValues()
        {
            _roll = _cockpit.Roll;
            _pitch = _cockpit.Pitch;
            _yaw = _controller.YRotation;
            _altitude = _controller.transform.position.y;
        }

        private void setTransformValues()
        {
            Quaternion yawRotation = Quaternion.AngleAxis(_yaw, Vector3.up);
            Quaternion rollRotation = Quaternion.AngleAxis(-_roll, Vector3.forward);
            Quaternion pitchRotation = Quaternion.AngleAxis(-_pitch, Vector3.left);

            transform.rotation = yawRotation * pitchRotation * rollRotation;
            transform.position = new Vector3(0, _altitude, 0);
        }
    }
}