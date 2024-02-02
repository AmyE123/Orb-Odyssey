using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadsetController : MonoBehaviour
{
    [SerializeField] private GameObject _cameraPosition;


    void Start()
    {
    }

    void LateUpdate()
    {
        UpdateHeadsetPosition();
    }

    private void UpdateHeadsetPosition()
    {
        Vector3 targetPosition = _cameraPosition.transform.position;
        transform.position = targetPosition;
    }
}

