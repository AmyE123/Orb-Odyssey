namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// The class for the sweeper obstacle
    /// </summary>
    public class SweeperObstacle : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _rotationSpeed = 100f;

        private void FixedUpdate()
        {
            _rigidbody.angularVelocity = Vector3.up * _rotationSpeed * Time.fixedDeltaTime;
        }
    }
}