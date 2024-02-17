namespace CT6RIGPR
{
    using UnityEngine;

    /// <summary>
    /// A class for dealing with timer buttons.
    /// </summary>
    public class TimerButton : MonoBehaviour
    {
        [Header("Timer Settings")]
        [SerializeField] private float _countdownDuration = 5.0f;
        [SerializeField] private float _timer;
        [SerializeField] private bool _isCountingDown = false;

        [Header("Visuals")]
        [SerializeField] private Material _greenMaterial;
        [SerializeField] private Material _redMaterial;

        [Header("Renderer")]
        [SerializeField] private MeshRenderer _meshRenderer;

        void Start()
        {
            if (_greenMaterial == null || _redMaterial == null)
            {
                Debug.LogError("[CT6RIGPR]: Materials are not set correctly for the timer button. Please set these in the inspector.");
            }

            if (_meshRenderer == null)
            {
                Debug.LogWarning("[CT6RIGPR]: MeshRenderer component not set for the timer button. Please set this in the inspector.");
                _meshRenderer = GetComponent<MeshRenderer>();
            }
         
            ResetButton();
        }

        void Update()
        {
            if (_isCountingDown)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    ResetButton();
                }
            }
        }

        private void ResetButton()
        {
            _timer = _countdownDuration;
            _isCountingDown = false;
            _meshRenderer.material = _greenMaterial;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PLAYER_TAG))
            {
                StartCountdown();
            }
        }

        private void StartCountdown()
        {
            _isCountingDown = true;
            _meshRenderer.material = _redMaterial;
        }
    }
}