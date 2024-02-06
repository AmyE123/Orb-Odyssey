namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// This is the trigger button for the tube.
    /// </summary>
    public class TubeTrigger : MonoBehaviour
    {
        [SerializeField] private SplineFollower _splineFollower;

        private void Start()
        {
            if (_splineFollower == null)
            {
                Debug.LogWarning("[CT6RIGPR] SplineFollower not set for " + transform.parent.name + " Please set this in the inspector.");
                _splineFollower = GetComponentInParent<SplineFollower>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PLAYER_TAG))
            {
                _splineFollower.StartFollowing();
            }
        }
    }
}