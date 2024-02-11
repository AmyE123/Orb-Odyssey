namespace CT6RIGPR
{
    using UnityEngine;
    using DG.Tweening;

    /// <summary>
    /// Used for the tube mechanic to move the player along a spline and disable their movement controls.
    /// </summary>
    public class SplineFollower : MonoBehaviour
    {
        private BallController _ballController;
        private float _splineProgress = 0f;
        private bool _isFollowing = false;

        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Transform _spline;
        [SerializeField] private float _speed = 0.1f;

        private void Start()
        {
            if (_gameManager == null)
            {
                _gameManager = FindObjectOfType<GameManager>();
                Debug.LogWarning("[CT6RIGPR] GameManager reference in Tube not set. Please set this in the inspector.");
            }

            _ballController = _gameManager.GlobalReferences.BallController;
        }

        public void StartFollowing()
        {
            _isFollowing = true;
            _ballController.DisableInput();
        }

        private void Update()
        {
            if (_isFollowing)
            {
                TubeSpline spline = _spline.GetComponent<TubeSpline>();

                _splineProgress += _speed * Time.deltaTime;
                _splineProgress = Mathf.Clamp(_splineProgress, 0f, 1f);

                Vector3 newPosition = spline.GetPointAt(_splineProgress);
                Quaternion newRotation = Quaternion.LookRotation(spline.GetDirectionAt(_splineProgress));
                Transform playerTransform = _ballController.gameObject.transform;
                playerTransform.position = newPosition;
                playerTransform.rotation = newRotation;

                if (_splineProgress >= 1.0f)
                {
                    _isFollowing = false;
                    _ballController.EnableInput();
                    _splineProgress = 0;

                    Rigidbody rb = _ballController.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                    }
                }
            }
        }
    }
}