namespace CT6RIGPR
{
    using UnityEngine;

    public class PlatformButton : MonoBehaviour
    {
        [SerializeField] private MovingPlatform _platform;
        [SerializeField] private Material _buttonPressedMat;
        [SerializeField] private MeshRenderer _buttonMR;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PLAYER_TAG))
            {
                _buttonMR.material = _buttonPressedMat;
                _platform.SetMovingPlatformActive();
            }
        }
    }
}