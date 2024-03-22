namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// The class for the swinging obstacle
    /// </summary>
    public class SwingingObstacle : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _swingForce = 100f;
        [SerializeField] private Vector3 _swingAxis = Vector3.forward;
        private Vector3 _initialDirection = Vector3.right;

        private void Start()
        {
            _rigidbody.AddForce(_initialDirection * _swingForce, ForceMode.Impulse);
        }

        private void FixedUpdate()
        {
            ApplyContinuousForce();
        }

        private void ApplyContinuousForce()
        {
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(_rigidbody.velocity, _swingAxis);
            Vector3 swingDirection = Vector3.Cross(_swingAxis, projectedVelocity).normalized;
            _rigidbody.AddForce(swingDirection * _swingForce * Time.fixedDeltaTime, ForceMode.Force);
        }
    }
}