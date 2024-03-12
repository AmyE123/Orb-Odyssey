namespace CT6RIGPR
{
    using Dreamteck.Splines;
    using UnityEngine;

    /// <summary>
    /// Additional functionalities for splines to help with Tube functionality.
    /// </summary>
    public class RIGPRSpline : MonoBehaviour
    {
        [SerializeField] private SplineComputer _splineComputer;

        /// <summary>
        /// Gets a point in the spline.
        /// </summary>
        public Vector3 GetPointAt(float t)
        {
            return _splineComputer.EvaluatePosition(t);
        }

        /// <summary>
        /// Gets a direction in the spline.
        /// </summary>
        public Vector3 GetDirectionAt(float t, float lookAhead = 0.01f)
        {
            Vector3 pointAtT = _splineComputer.EvaluatePosition(t);
            Vector3 pointAhead = _splineComputer.EvaluatePosition(Mathf.Clamp01(t + lookAhead));
            return (pointAhead - pointAtT).normalized;
        }

        private void Start()
        {
            if (_splineComputer == null)
            {
                Debug.LogWarning("[CT6RIGPR]: SplineComputer reference not set for a spline in the scene.");
            }
        }

        private void OnDrawGizmos()
        {
            if (_splineComputer != null)
            {
                Gizmos.color = Color.red;

                float step = 0.05f;

                for (float i = 0; i < 1; i += step)
                {
                    Vector3 startPosition = _splineComputer.EvaluatePosition(i);
                    Vector3 endPosition = _splineComputer.EvaluatePosition(i + step);

                    if (i + step > 1)
                    {
                        endPosition = _splineComputer.EvaluatePosition(1);
                    }

                    Gizmos.DrawLine(startPosition, endPosition);
                }
            }
        }
    }
}