namespace CT6RIGPR
{
    using DG.Tweening;
    using UnityEngine;

    public abstract class PowerupObjectBase : MonoBehaviour
    {
        private bool _hasBeenPickedUp = false;
        private AudioSource _audioSource;
        [SerializeField] private CollectableData _collectableData;
        [SerializeField] private GameObject _powerupVisualPrefab;
        [SerializeField] private MeshRenderer _powerupMeshRenderer;

        /// <summary>
        /// The visual prefab of the powerup. For use with inventory.
        /// </summary>
        public GameObject PowerupVisualPrefab => _powerupVisualPrefab;

        /// <summary>
        /// Bool for whether the powerup has been picked up.
        /// </summary>
        public bool HasBeenPickedUp => _hasBeenPickedUp;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Allows the powerup to be picked up again, as well as resetting it visually.
        /// </summary>
        public void ResetPowerUp()
        {
            _hasBeenPickedUp = false;
            _powerupMeshRenderer.enabled = true;
            transform.localScale = Vector3.one;
        }

        protected virtual void PickUpPowerup(Collider player)
        {
            _audioSource.PlayOneShot(_collectableData.CollectableClip);
            transform.DOScale(Vector3.zero, _collectableData.ShrinkDuration)
            .OnComplete(() => {
                _powerupMeshRenderer.enabled = false;
            }).SetEase(_collectableData.ShrinkEase);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PLAYER_TAG) && !_hasBeenPickedUp)
            {
                _hasBeenPickedUp = true;
                PickUpPowerup(other);
            }
        }
    }
}