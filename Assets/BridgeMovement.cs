namespace CT6RIGPR
{
    using DG.Tweening;
    using Unity.VisualScripting;
    using UnityEngine;

    public class BridgeMovement : MonoBehaviour
    {
        [SerializeField] private GameObject _platform;
        [SerializeField] private float _yMin, _yMax;
        [SerializeField] private bool _isRaised = false;

        private void Start()
        {
            InitialisePlatform();
        }

        private void InitialisePlatform()
        {
            _platform.transform.localPosition = new Vector3 (_platform.transform.localPosition.x, _yMin, _platform.transform.localPosition.z);
        }

        public void ToggleBridge()
        {

        }


    }
}

