using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeButton : MonoBehaviour
{
    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private AudioSource buttonSource;
    private bool isRaised;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isRaised == false)
        {
            buttonAnimator.SetTrigger("RaiseSection");
            isRaised = true;
            buttonSource.Play();
        }

        else if (collision.gameObject.CompareTag("Player") && isRaised == true)
        {
            buttonAnimator.SetTrigger("LowerSection");
            isRaised = false;
            buttonSource.Play();
        }
    }
}
