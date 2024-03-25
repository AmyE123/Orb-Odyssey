namespace CT6RIGPR
{
    using UnityEngine;
    using static CT6RIGPR.Constants;

    /// <summary>
    /// Used for the jump pad in the game
    /// </summary>
    public class JumpPad : MonoBehaviour
    {
        private GameManager _gameManager;
        private GlobalGameReferences _globalRef;

        [SerializeField] private float _verticalJumpForce = 10f;
        [SerializeField] private float _horizontalJumpForce = 0f;
        [SerializeField] private JumpPadDirection _launchDirection = JumpPadDirection.Up;

        private void Start()
        {
            if (_gameManager == null)
            {
                _gameManager = FindObjectOfType<GameManager>();
                _globalRef = _gameManager.GlobalGameReferences;
            }
        }

        /// <summary>
        /// Set the launch direction for the jump pad.
        /// </summary>
        /// <param name="direction">The direction wanted.</param>
        public void SetLaunchDirection(JumpPadDirection direction)
        {
            _launchDirection = direction;
        }

        /// <summary>
        /// Get the current launch direction for the jump pad.
        /// </summary>
        /// <returns>A vector 3 of the launch direction.</returns>
        public Vector3 GetLaunchDirection()
        {
            switch (_launchDirection)
            {
                case JumpPadDirection.Up:
                    return Vector3.up;
                case JumpPadDirection.Down:
                    return Vector3.down;
                case JumpPadDirection.Left:
                    return Vector3.left;
                case JumpPadDirection.Right:
                    return Vector3.right;
                case JumpPadDirection.Forward:
                    return transform.forward;
                case JumpPadDirection.Backward:
                    return -transform.forward;
                default:
                    return Vector3.up;
            }
        }

        private bool SideWaysJumpPad()
        {
            return (_launchDirection != JumpPadDirection.Up && _launchDirection != JumpPadDirection.Down);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody != null)
            {
                _globalRef.GameSFXManager.PlayJumpPadSound();
                if (SideWaysJumpPad())
                {
                    other.attachedRigidbody.AddForce(GetLaunchDirection() * _horizontalJumpForce, ForceMode.VelocityChange);
                    other.attachedRigidbody.AddForce(Vector3.up * _verticalJumpForce, ForceMode.VelocityChange);
                }
                else
                {
                    other.attachedRigidbody.AddForce(GetLaunchDirection() * _verticalJumpForce, ForceMode.VelocityChange);
                }
            }
        }
    }
}