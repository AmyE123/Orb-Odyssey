using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeButton : MonoBehaviour
{
    [SerializeField] private Animator section1Animator;
    [SerializeField] private AudioSource buttonSource;
    public bool section3IsRaised;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && section3IsRaised == false)
        {
            //Section1
            section1Animator.SetTrigger("RaiseSection");
            section3IsRaised = true;
            buttonSource.Play();
        }

        else if (collision.gameObject.CompareTag("Player") && section3IsRaised == true)
        {
            //Section1
            section1Animator.SetTrigger("LowerSection");
            section3IsRaised = false;
            buttonSource.Play();
        }
    }
}
