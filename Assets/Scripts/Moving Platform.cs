using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject _startLocation, _endLocation, _platform;
    [SerializeField] private float _speed = 1.0f;

    private void Update()
    {
        _platform.transform.position = Vector3.Lerp(_startLocation.transform.position, _endLocation.transform.position, Mathf.PingPong(Time.deltaTime / _speed, 1));
    }

}
