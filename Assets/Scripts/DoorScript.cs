using UnityEngine;
using CT6RIGPR;
using DG.Tweening;
using UnityEngine.InputSystem.LowLevel;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private GameObject _doorHinge;
    [SerializeField] private float _doorRotation;
    [SerializeField] private bool _doorClosed = true;
    [SerializeField] private float _doorOpenSpeed = 1f;
    private float _startDoorRotation;

    public float DoorOpenDuration = 2f;

    private void Start()
    {
        InitialiseDoor();
    }

    private void InitialiseDoor()
    {
        _startDoorRotation = _doorHinge.transform.localEulerAngles.y;
    }

    public void ToggleDoor()
    {
        if (_doorClosed)
        {
            Vector3 openRotation = new Vector3(_doorHinge.transform.localEulerAngles.x, _doorRotation, _doorHinge.transform.localEulerAngles.z);
            _doorHinge.transform.DOLocalRotate(openRotation, _doorOpenSpeed);
            _doorClosed = false;
        }
    }
}
