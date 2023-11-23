namespace CT6RIGPR
{
    using UnityEngine;
    using Actuate;

    /// <summary>
    /// Manages the motion source for the ball through the actuate agent
    /// </summary>
    public class BallMotionSource : MonoBehaviour
    {
        [SerializeField] private GameObject _ballGameObject;
        [SerializeField] private ActuateAgent _actuateAgent;

        void Start()
        {
            _actuateAgent.SetMotionSource(this.gameObject);
        }

        void FixedUpdate()
        {
            UpdateBallPosition();
        }

        private void UpdateBallPosition()
        {
            transform.position = _ballGameObject.transform.position;
        }
    }
}