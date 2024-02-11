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

        /// <summary>
        /// Start following the spline. This is called when the trigger is activated.
        /// </summary>
        public void StartFollowing()
        {
            _isFollowing = true;
            _ballController.DisableInput();
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
                TubeSpline spline = _spline.GetComponent<TubeSpline>();

                Transform playerTransform = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).transform;
                Transform cockpitTransform = GameObject.FindGameObjectWithTag(Constants.COCKPIT_TAG).transform;
                BallController ballScript = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).GetComponent<BallController>();
                BallCockpit cockpitScript = GameObject.FindGameObjectWithTag(Constants.COCKPIT_TAG).GetComponent<BallCockpit>();

                _splineProgress += _speed * Time.deltaTime;

                if (_splineProgress >= 1.0f)
                {
                    _isFollowing = false;
                    _splineProgress = 1.0f;

                    _ballController.EnableInput();

                    // Assuming you use the last point on the spline as the exit point.
                    Vector3 exitPosition = spline.GetPointAt(1.0f); // Get the final point on the spline.
                    Quaternion exitRotation = Quaternion.LookRotation(spline.GetDirectionAt(1.0f));

                    // Move the player and cockpit to the exit position and adjust orientation.
                    playerTransform.position = exitPosition; // For an instant move, or use DOMove for a smooth transition.
                    playerTransform.rotation = exitRotation;

                    cockpitTransform.position = exitPosition;
                    cockpitTransform.rotation = exitRotation;

                    // Reset spline progress for next use.
                    _splineProgress = 0;
                }

                Vector3 newPosition = spline.GetPointAt(_splineProgress);

                playerTransform.DOMove(newPosition, 1f);
                cockpitTransform.DOMove(newPosition, 1f);

                Vector3 splineEulers = Quaternion.LookRotation(spline.GetDirectionAt(_splineProgress)).eulerAngles;
                ballScript._yRotation = splineEulers.y;
                cockpitScript.SetRoll(splineEulers.x);
                cockpitScript.SetPitch(splineEulers.z);
            }
        }
    }
}