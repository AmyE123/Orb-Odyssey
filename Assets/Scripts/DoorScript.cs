using UnityEngine;
using CT6RIGPR;
using DG.Tweening;
using UnityEngine.InputSystem.LowLevel;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private GameObject _doorHinge;
    [SerializeField] private float _doorRotation;
    [SerializeField] private bool _doorClosed;
    [SerializeField] private float _doorOpenSpeed = 1f;
    private float _startDoorRotation;

    private void Start()
    {
        InitialiseDoor();
    }

    private void InitialiseDoor()
    {
        _startDoorRotation = _doorHinge.transform.localRotation.y;
    }

    public void ToggleDoor()
    {
        if (_doorClosed == true)
        {
            _doorHinge.transform.DOLocalRotate(new Vector3(_doorHinge.transform.rotation.x, _doorRotation, _doorHinge.transform.rotation.z), _doorOpenSpeed);
            _doorClosed = false;
        }

        else
        {
            _doorHinge.transform.DOLocalRotate(new Vector3(_doorHinge.transform.rotation.x, _startDoorRotation, _doorHinge.transform.rotation.z), _doorOpenSpeed);
            _doorClosed = true;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space has been pressed");
            ToggleDoor();
        }
    }
}
