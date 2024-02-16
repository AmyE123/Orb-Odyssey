namespace CT6RIGPR
{
    using DG.Tweening;
    using UnityEngine;

    public abstract class PowerupObjectBase : MonoBehaviour
    {
        bool _hasBeenPickedUp = false;
        [SerializeField] private PickupData _pickupData;

        protected virtual void PickUpPowerup(Collider player)
        {
            transform.DOScale(Vector3.zero, _pickupData.ShrinkDuration)
            .OnComplete(() => {
                Destroy(gameObject, _pickupData.DestroyDelay);
            }).SetEase(_pickupData.ShrinkEase);
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