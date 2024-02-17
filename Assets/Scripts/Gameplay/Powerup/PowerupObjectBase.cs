namespace CT6RIGPR
{
    using DG.Tweening;
    using UnityEngine;

    public abstract class PowerupObjectBase : MonoBehaviour
    {
        bool _hasBeenPickedUp = false;
        private AudioSource _audioSource;
        [SerializeField] private CollectableData _collectableData;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        protected virtual void PickUpPowerup(Collider player)
        {
            _audioSource.PlayOneShot(_collectableData.CollectableClip);

            transform.DOScale(Vector3.zero, _collectableData.ShrinkDuration)
            .OnComplete(() => {
                Destroy(gameObject, _collectableData.DestroyDelay);
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