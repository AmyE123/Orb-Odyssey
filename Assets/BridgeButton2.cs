using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BridgeButton2 : MonoBehaviour
{
    [SerializeField] private Animator section1Animator, section3Animator;
    public bool section3IsRaised;
    [SerializeField] private BridgeButton bridgeButton1Script;
    [SerializeField] private AudioSource buttonSource;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (bridgeButton1Script.section3IsRaised, section3IsRaised)
            {
                case (false, false):

                    //Section1
                    section1Animator.SetTrigger("RaiseSection");
                    bridgeButton1Script.section3IsRaised = true;

                    //Section3
                    section3Animator.SetTrigger("RaiseSection3");
                    section3IsRaised = true;

                    buttonSource.Play();
                    break;


                case (true, false):

                    //Section1
                    section1Animator.SetTrigger("LowerSection");
                    bridgeButton1Script.section3IsRaised = false;

                    //Section3
                    section3Animator.SetTrigger("RaiseSection3");
                    section3IsRaised = true;

                    buttonSource.Play();
                    break;

                case (false, true):

                    //Section1
                    section1Animator.SetTrigger("RaiseSection");
                    bridgeButton1Script.section3IsRaised = true;

                    //Section3
                    section3Animator.SetTrigger("LowerSection3");
                    section3IsRaised = false;

                    buttonSource.Play();
                    break;

                case (true, true):
                    //Section1
                    section1Animator.SetTrigger("LowerSection");
                    bridgeButton1Script.section3IsRaised = false;

                    //Section3
                    section3Animator.SetTrigger("LowerSection3");
                    section3IsRaised = false;

                    buttonSource.Play();
                    break;
            }
        }
    }
}
