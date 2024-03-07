namespace CT6RIGPR
{
    using DG.Tweening;
    using UnityEngine;

    public class BridgeMovement : MonoBehaviour
    {
        [SerializeField] private GameObject _platform;
        [SerializeField] private float _yMin, _yMax;
        [SerializeField] private bool _isRaised = false;
        [SerializeField] private float _speed = 10f;

        private void Start()
        {
            InitialisePlatform();
        }

        private void InitialisePlatform()
        {
            _platform.transform.localPosition = new Vector3(_platform.transform.localPosition.x, _platform.transform.localPosition.y, _platform.transform.localPosition.z);
        }

        public void ToggleBridge()
        {
            if (_isRaised == false)
            {
                transform.DOLocalMoveY(_yMax, _speed);
                _isRaised = true;
            }
            else
            {
                transform.DOLocalMoveY(_yMin, _speed);
                _isRaised = false;
            }
        }
    }
}

