using CT6RIGPR;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpObjectBase : MonoBehaviour
{
    bool _hasBeenPickedUp = false;
    [SerializeField] private PickupData _pickupData;

    protected virtual void PickUpPowerUp(Collider player)
    {
        transform.DOScale(Vector3.zero, _pickupData.ShrinkDuration)
        .OnComplete(() => {
        Destroy(gameObject, _pickupData.DestroyDelay);
        }).SetEase(_pickupData.ShrinkEase);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_hasBeenPickedUp)
        {
            _hasBeenPickedUp = true;
            PickUpPowerUp(other);
        }
    }
}
