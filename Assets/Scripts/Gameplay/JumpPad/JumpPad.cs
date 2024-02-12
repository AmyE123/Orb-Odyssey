namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// Used for the jump pad in the game
    /// </summary>
    public class JumpPad : MonoBehaviour
    {
        [SerializeField] private float _jumpForce = 10f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody != null)
            {
                other.attachedRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
            }
        }
    }
}