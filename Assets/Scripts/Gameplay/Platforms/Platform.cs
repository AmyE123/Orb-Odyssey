namespace CT6RIGPR
{
    using UnityEngine;

    public class Platform : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PLAYER_TAG))
            {
                other.transform.SetParent(transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Constants.PLAYER_TAG))
            {
                other.transform.SetParent(null);
            }
        }
    }
}