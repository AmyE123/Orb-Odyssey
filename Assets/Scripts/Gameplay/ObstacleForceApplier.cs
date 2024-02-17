namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// A class that deals with adding force to things hit with swinging/sweeping objects
    /// </summary>
    public class ObstacleForceApplier : MonoBehaviour
    {
        [SerializeField] private float _hitForce = 50f;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Constants.PLAYER_TAG))
            {
                Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    Vector3 forceDirection = (collision.transform.position - transform.position).normalized;
                    float forceMultiplier = _hitForce;
                    playerRb.AddForce(forceDirection * forceMultiplier, ForceMode.Impulse);
                }
            }
        }
    }
}