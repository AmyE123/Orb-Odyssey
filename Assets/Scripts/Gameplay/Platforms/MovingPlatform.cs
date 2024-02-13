namespace CT6RIGPR
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// A class that manages moving platforms in the game.
    /// </summary>
    public class MovingPlatform : MonoBehaviour
    {
        private float _splineProgress = 0f;
        private bool _isMovingForward = true;
        private bool _isWaiting = false;
        private bool _startPositionSet = false;

        [Header("Spline & Platform Configuration")]
        [SerializeField] private Transform _spline;        
        [SerializeField] private Transform _platformTransform;     

        [Header("Moving Platform Settings")]
        [SerializeField] private float _platformSpeed = 0.1f;
        [SerializeField] private float _waitTime = 2f;
        [SerializeField] private bool _isActivated = false;

        /// <summary>
        /// A bool indicating whether the moving platform is activated or not.
        /// </summary>
        public bool IsActivated => _isActivated;

        /// <summary>
        /// Set the moving platform to active.
        /// </summary>
        public void SetMovingPlatformActive()
        {
            _isActivated = true;
        }        

        private void Start()
        {
            InitializeStartPosition();
        }

        private void Update()
        {
            if (_isActivated)
            {
                _startPositionSet = false;

                if (!_isWaiting)
                {
                    UpdateMovement();
                }
            }
            else
            {
                if (!_startPositionSet)
                {
                    InitializeStartPosition();
                }
            }
        }

        private void InitializeStartPosition()
        {            
            RIGPRSpline splineComponent = _spline.GetComponent<RIGPRSpline>();

            Vector3 newPosition = splineComponent.GetPointAt(0);
            Quaternion newRotation = Quaternion.LookRotation(splineComponent.GetDirectionAt(_splineProgress));
            _platformTransform.transform.position = newPosition;
            _platformTransform.transform.rotation = newRotation;

            _splineProgress = 0;
            _startPositionSet = true;
        }

        private void UpdateMovement()
        {
            RIGPRSpline splineComponent = _spline.GetComponent<RIGPRSpline>();

            if (_isMovingForward)
            {
                if (_splineProgress < 1.0f)
                {
                    _splineProgress += _platformSpeed * Time.deltaTime;
                    _splineProgress = Mathf.Min(_splineProgress, 1.0f);
                }
                if (_splineProgress >= 1.0f && !_isWaiting)
                {
                    StartCoroutine(WaitAtEnd());
                }
            }
            else
            {
                if (_splineProgress > 0f)
                {
                    _splineProgress -= _platformSpeed * Time.deltaTime;
                    _splineProgress = Mathf.Max(_splineProgress, 0f);
                }
                if (_splineProgress <= 0f && !_isWaiting)
                {
                    StartCoroutine(WaitAtEnd());
                }
            }

            Vector3 newPosition = splineComponent.GetPointAt(_splineProgress);
            _platformTransform.transform.position = newPosition;

            Vector3 forwardDirection = splineComponent.GetDirectionAt(_splineProgress);
            Vector3 projectedForward = new Vector3(forwardDirection.x, forwardDirection.y, 0).normalized;
            if (projectedForward.magnitude > 0)
            {
                //Quaternion newRotation = Quaternion.LookRotation(projectedForward, Vector3.up);
                _platformTransform.transform.rotation = Quaternion.Euler(_platformTransform.transform.rotation.eulerAngles.x, _platformTransform.transform.rotation.eulerAngles.y, 0);
            }
        }

        private IEnumerator WaitAtEnd()
        {
            _isWaiting = true;
            yield return new WaitForSeconds(_waitTime);
            _isMovingForward = !_isMovingForward;
            _isWaiting = false;
        }
    }
}