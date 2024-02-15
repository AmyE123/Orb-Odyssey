using CT6RIGPR;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BridgeButton : MonoBehaviour
{
    [SerializeField] private BridgeMovement[] _bridgeSections;
    [SerializeField] int _buttonNumber;
    [SerializeField] private AudioSource _buttonSound;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _buttonSound.Play();

            switch (_buttonNumber)
            {
                case (1):
                    _bridgeSections[0].ToggleBridge();
                    break;
                case (2):
                    _bridgeSections[0].ToggleBridge();
                    _bridgeSections[2].ToggleBridge();
                    break;
                case (3):
                    _bridgeSections[2].ToggleBridge();
                    _bridgeSections[3].ToggleBridge();
                    break;
                case (4):
                    _bridgeSections[0].ToggleBridge();
                    _bridgeSections[1].ToggleBridge();
                    _bridgeSections[3].ToggleBridge();
                    break;
            }
        }

    }
}
