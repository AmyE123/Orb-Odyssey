namespace CT6RIGPR
{
    using Unity.Mathematics;
    using UnityEngine;
    using UnityEngine.Splines;

    /// <summary>
    /// Additional functionalities for splines to help with Tube functionality.
    /// </summary>
    public class TubeSpline : MonoBehaviour
    {
        private Spline _spline;

        [SerializeField] private SplineContainer _splineContainer;

        void Start()
        {
            if (_splineContainer != null)
            {
                _spline = _splineContainer.Spline;
            }
        }

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
    }
}