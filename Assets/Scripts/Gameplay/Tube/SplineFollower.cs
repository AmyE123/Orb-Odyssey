namespace CT6RIGPR
{
    using UnityEngine;
    using UnityEngine.Rendering;
    using DG.Tweening;

    /// <summary>
    /// Used for the tube mechanic to move the player along a spline and disable their movement controls.
    /// </summary>
    public class SplineFollower : MonoBehaviour
    {
        private BallController _ballController;

        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Transform _spline;
        [SerializeField] private float _speed = 5.0f;
        [SerializeField] private float _splineProgress = 0f;
        [SerializeField] private bool _isFollowing = false;

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
            }
            
            _ballController = _gameManager.GlobalReferences.BallController;
        }

        private void Update()
        {
            if (_isFollowing)
            {
                _splineProgress += _speed * Time.deltaTime;

                if (_splineProgress >= 1.0f)
                {
                    _isFollowing = false;
                    _splineProgress = 1.0f;

                    _ballController.EnableInput();
                    _splineProgress = 0;
                }

                TubeSpline spline = _spline.GetComponent<TubeSpline>();

                Transform playerTransform = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).transform;
                Transform cockpitTransform = GameObject.FindGameObjectWithTag(Constants.COCKPIT_TAG).transform;
                BallController ballScript = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).GetComponent<BallController>();
                BallCockpit cockpitScript = GameObject.FindGameObjectWithTag(Constants.COCKPIT_TAG).GetComponent<BallCockpit>();

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