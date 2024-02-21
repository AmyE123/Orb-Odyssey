using CT6RIGPR;
using UnityEngine;
using UnityEngine.Rendering;

public class BridgeButton : MonoBehaviour
{
    [SerializeField] private BridgeMovement[] _bridgeSections;
    [SerializeField] int _buttonNumber;
    [SerializeField] private AudioSource _buttonSound;
    [Header("Materials")]
    [SerializeField] private Material _offMaterial, _onMaterial;
    [SerializeField] BridgeButtonManager _managerScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _buttonSound.Play();

            switch (_buttonNumber)
            {
                case (1):
                    //Bridges
                    _bridgeSections[0].ToggleBridge();
                    //Light 0
                    ToggleLights(0);
                    break;
                case (2):
                    //Bridges
                    _bridgeSections[0].ToggleBridge();
                    _bridgeSections[2].ToggleBridge();
                    //Light 0
                    ToggleLights(0);
                    //Light 2
                    ToggleLights(2);
                    break;
                case (3):
                    //Bridges
                    _bridgeSections[2].ToggleBridge();
                    _bridgeSections[3].ToggleBridge();
                    //Light 2
                    ToggleLights(2);
                    //Light 3
                    ToggleLights(3);
                    break;
                case (4):
                    //Bridges
                    _bridgeSections[0].ToggleBridge();
                    _bridgeSections[1].ToggleBridge();
                    _bridgeSections[3].ToggleBridge();
                    //Light 0
                    ToggleLights(0);
                    //Light 1
                    ToggleLights(1);
                    //Light 3
                    ToggleLights(3);
                    break;
            }
        }

    }

    private void ToggleLights(int lightNumber)
    {
        switch (lightNumber)
        {
            case 0:
                if (_bridgeSections[0]._isRaised)
                {
                    for (int i = 0; i < _managerScript.light0.Length; i++)
                    {
                        _managerScript.light0[i].material = _onMaterial;
                    }
                }
                else if (!_bridgeSections[0]._isRaised)
                {
                    for (int i = 0; i < _managerScript.light0.Length; i++)
                    {
                        _managerScript.light0[i].material = _offMaterial;

                    }
                }
                break;

            case 1:
                if (_bridgeSections[1]._isRaised)
                {
                    for (int i = 0; i < _managerScript.light1.Length; i++)
                    {
                        _managerScript.light1[i].material = _onMaterial;
                    }
                }
                else if ((!_bridgeSections[1]._isRaised))
                {
                    for (int i = 0; i < _managerScript.light1.Length; i++)
                    {
                        _managerScript.light1[i].material = _offMaterial;

                    }
                }
                break;

            case 2:
                if (_bridgeSections[2]._isRaised)
                {
                    for (int i = 0; i < _managerScript.light2.Length; i++)
                    {
                        _managerScript.light2[i].material = _onMaterial;
                    }
                }
                else if (!_bridgeSections[2]._isRaised)
                {
                    for (int i = 0; i < _managerScript.light2.Length; i++)
                    {
                        _managerScript.light2[i].material = _offMaterial;

                    }
                }
                break;

            case 3:
                if (_bridgeSections[3]._isRaised)
                {
                    for (int i = 0; i < _managerScript.light3.Length; i++)
                    {
                        _managerScript.light3[i].material = _onMaterial;
                    }
                }
                else if (!_bridgeSections[3]._isRaised)
                {
                    for (int i = 0; i < _managerScript.light3.Length; i++)
                    {
                        _managerScript.light3[i].material = _offMaterial;
                    }
                }
                break;
        }

    }
}
