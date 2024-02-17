namespace CT6RIGPR
{
    using System.Collections;
    using UnityEngine;

    public class TimerSplinePathFollower : MonoBehaviour
    {
        [SerializeField] private TimerButton _timerButton;
        [SerializeField] private Transform _spline;
        [SerializeField] private Transform _objectToMove;
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _moveDelay = 3f;

        private RIGPRSpline _splineComponent;
        private float _splineProgress = 0f;
        private bool _isMovingForward = true;        

        private void Start()
        {
            _splineComponent = _spline.GetComponent<RIGPRSpline>();
            _timerButton.OnTimerStarted += () => StartMoving(true);
            _timerButton.OnTimerFinished += () => StartMoving(false);
        }

        private void StartMoving(bool forward)
        {
            _isMovingForward = forward;
            StopAllCoroutines();
            StartCoroutine(MoveAlongSpline());
        }

        private IEnumerator MoveAlongSpline()
        {
            yield return new WaitForSeconds(_moveDelay);

            while ((_isMovingForward && _splineProgress < 1.0f) || (!_isMovingForward && _splineProgress > 0f))
            {
                _splineProgress += (_isMovingForward ? 1 : -1) * _moveSpeed * Time.deltaTime;
                _splineProgress = Mathf.Clamp(_splineProgress, 0f, 1f);

                Vector3 newPosition = _splineComponent.GetPointAt(_splineProgress);
                _objectToMove.position = newPosition;

                // Vector3 newDirection = _splineComponent.GetDirectionAt(_splineProgress);
                // _objectToMove.rotation = Quaternion.LookRotation(newDirection);

                yield return null;
            }
        }      

        private void OnDestroy()
        {
            _timerButton.OnTimerStarted -= () => StartMoving(true);
            _timerButton.OnTimerFinished -= () => StartMoving(false);
        }
    }
}