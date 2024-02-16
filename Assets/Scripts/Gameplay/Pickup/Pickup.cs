namespace CT6RIGPR
{
    using UnityEngine;
    using DG.Tweening;

    public class Pickup : MonoBehaviour
    {
        private bool _hasBeenPickedUp = false;
        private AudioSource _audioSource;
        private GameManager _gameManager;

        [SerializeField] private PickupData _pickupData;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.PLAYER_TAG) && !_hasBeenPickedUp)
            {
                _hasBeenPickedUp = true;
                PickupObject();
            }
        }


        private void PickupObject()
        {
            _gameManager.IncrementPickupCount();
            _audioSource.PlayOneShot(_pickupData.PickupClip);

            transform.DOScale(Vector3.zero, _pickupData.ShrinkDuration)
                .OnComplete(() => {
                    Destroy(gameObject, _pickupData.DestroyDelay);
                }).SetEase(_pickupData.ShrinkEase);
        }
    }
}