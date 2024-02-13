using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeButton3 : MonoBehaviour
{
    [SerializeField] private Animator section4Animator, section3Animator;
    public bool section4IsRaised;
    [SerializeField] private BridgeButton bridgeButton2Script;
    [SerializeField] private AudioSource buttonSource;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (bridgeButton2Script.section3IsRaised, section4IsRaised)
            {
                case (false, false):

                    //Section3
                    section3Animator.SetTrigger("RaiseSection3");
                    bridgeButton2Script.section3IsRaised = true;

                    //Section4
                    section4Animator.SetTrigger("RaiseSection4");
                    section4IsRaised = true;

                    buttonSource.Play();
                    break;


                case (true, false):

                    //Section3
                    section3Animator.SetTrigger("LowerSection3");
                    bridgeButton2Script.section3IsRaised = false;

                    //Section4
                    section4Animator.SetTrigger("RaiseSection4");
                    section4IsRaised = true;

                    buttonSource.Play();
                    break;

                case (false, true):

                    //Section3
                    section3Animator.SetTrigger("RaiseSection3");
                    bridgeButton2Script.section3IsRaised = true;

                    //Section4
                    section4Animator.SetTrigger("LowerSection4");
                    section4IsRaised = false;

                    buttonSource.Play();
                    break;

                case (true, true):
                    //Section3
                    section3Animator.SetTrigger("LowerSection3");
                    bridgeButton2Script.section3IsRaised = false;

                    //Section4
                    section4Animator.SetTrigger("LowerSection4");
                    section4IsRaised = false;

                    buttonSource.Play();
                    break;
            }
        }
    }
}
