namespace CT6RIGPR
{
    using UnityEngine;
    using static CT6RIGPR.Constants;

    /// <summary>
    /// Used for the tube mechanic to move the player along a spline and disable their movement controls.
    /// </summary>
    public class TubeFollower : MonoBehaviour
    {
        private BallController _ballController;
        private float _splineProgress = 0f;
        private bool _isFollowing = false;

        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Direction _activeMovingDirection = Direction.Forward;

        [Header("Spline Values")]
        [SerializeField] private Transform _spline;
        [SerializeField] private float _speed = 0.1f;       

        /// <summary>
        /// Activate the trigger for the tube teleportation
        /// </summary>
        /// <param name="triggerDirection">The direction to travel the tube in.</param>
        public void ActivateTrigger(Direction triggerDirection)
        {
            StartFollowing(triggerDirection);
        }

        private void Start()
        {
            if (_gameManager == null)
            {
                _gameManager = FindObjectOfType<GameManager>();
                Debug.LogWarning("[CT6RIGPR] GameManager reference in Tube not set. Please set this in the inspector.");
            }

            _ballController = _gameManager.GlobalReferences.BallController;
        }

        private void Update()
        {
            if (_isFollowing)
            {
                UpdateMovement();
            }
        }

        private void StartFollowing(Direction followDirection)
        {
            _activeMovingDirection = followDirection;
            _isFollowing = true;
            _ballController.DisableInput();

            _splineProgress = (_activeMovingDirection == Direction.Forward) ? 0f : 1f;

            UpdateMovement();
        }

        private void UpdateMovement()
        {
            RIGPRSpline spline = _spline.GetComponent<RIGPRSpline>();

            if (_activeMovingDirection == Direction.Forward)
            {
                _splineProgress += _speed * Time.deltaTime;
                if (_splineProgress >= 1.0f)
                {
                    StopFollowing();
                }
            }
            else
            {
                _splineProgress -= _speed * Time.deltaTime;
                if (_splineProgress <= 0f)
                {
                    StopFollowing();
                }
            }

            _splineProgress = Mathf.Clamp(_splineProgress, 0f, 1f);

            Vector3 newPosition = spline.GetPointAt(_splineProgress);
            Quaternion newRotation = Quaternion.LookRotation(spline.GetDirectionAt(_splineProgress));
            Transform playerTransform = _ballController.gameObject.transform;
            playerTransform.position = newPosition;
            playerTransform.rotation = newRotation;
        }

        private void StopFollowing()
        {
            _isFollowing = false;
            _ballController.EnableInput();

            RIGPRSpline spline = _spline.GetComponent<RIGPRSpline>();
            if (_activeMovingDirection == Direction.Forward)
            {
                _splineProgress = 1f;
            }
            else
            {
                _splineProgress = 0f;
            }

            Vector3 finalPosition = spline.GetPointAt(_splineProgress);
            Quaternion finalRotation = Quaternion.LookRotation(spline.GetDirectionAt(_splineProgress));
            Transform playerTransform = _ballController.gameObject.transform;
            playerTransform.position = finalPosition;
            playerTransform.rotation = finalRotation;

            Rigidbody rb = _ballController.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}