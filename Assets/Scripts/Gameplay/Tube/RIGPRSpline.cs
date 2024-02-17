namespace CT6RIGPR
{
    using Unity.Mathematics;
    using UnityEngine;
    using UnityEngine.Splines;

    /// <summary>
    /// Additional functionalities for splines to help with Tube functionality.
    /// </summary>
    public class RIGPRSpline : MonoBehaviour
    {
        private Spline _spline;

        [SerializeField] private SplineContainer _splineContainer;

        /// <summary>
        /// Gets a point in the spline.
        /// </summary>
        public Vector3 GetPointAt(float t)
        {
            if (_spline == null) return Vector3.zero;

            return _spline.EvaluatePosition(t);
        }

        /// <summary>
        /// Gets a direction in the spline.
        /// </summary>
        public Vector3 GetDirectionAt(float t)
        {
            if (_spline == null) return Vector3.forward;

            float3 tangent = SplineUtility.EvaluateTangent(_spline, t);
            return new Vector3(tangent.x, tangent.y, tangent.z).normalized;
        }

        private void Start()
        {
            if (_splineContainer != null)
            {
                _spline = _splineContainer.Spline;
            }
        }

        private void OnDrawGizmos()
        {
            if (_splineContainer != null)
            {
                Gizmos.color = Color.red;

                float step = 0.05f;

                for (float i = 0; i < 1; i += step)
                {
                    Vector3 startPosition = _splineContainer.Spline.EvaluatePosition(i);
                    Vector3 endPosition = _splineContainer.Spline.EvaluatePosition(i + step);

                    if (i + step > 1)
                    {
                        endPosition = _splineContainer.Spline.EvaluatePosition(1);
                    }

                    Gizmos.DrawLine(startPosition, endPosition);
                }
            }
        }
    }
}