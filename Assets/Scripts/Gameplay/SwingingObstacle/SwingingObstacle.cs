namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// The class for the swinging obstacle
    /// </summary>
    public class SwingingObstacle : MonoBehaviour
    {
        [SerializeField] private float _swingingSpeed = 1f;
        [SerializeField] private float _swingingAngularLimit = 70f;


        private void Update()
        {
            float angle = _swingingAngularLimit * Mathf.Sin(Time.time * _swingingSpeed);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}