namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// The class for the swinging obstacle
    /// </summary>
    public class SwingingObstacle : MonoBehaviour
    {
        [SerializeField] private float speed = 1.5f;
        [SerializeField] private float limit = 75f;


        private void Update()
        {
            float angle = limit * Mathf.Sin(Time.time * speed);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}