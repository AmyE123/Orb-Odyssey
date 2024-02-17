namespace CT6RIGPR
{
    using UnityEngine;
    using DG.Tweening;

    public class Collectable : MonoBehaviour
    {
        private bool _hasBeenPickedUp = false;
        private AudioSource _audioSource;
        private GameManager _gameManager;

        [SerializeField] private CollectableData _collectableData;

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
            _gameManager.IncrementCollectableCount();
            _audioSource.PlayOneShot(_collectableData.CollectableClip);

            transform.DOScale(Vector3.zero, _collectableData.ShrinkDuration)
                .OnComplete(() => {
                    Destroy(gameObject, _collectableData.DestroyDelay);
                }).SetEase(_collectableData.ShrinkEase);
        }
    }
}