using CT6RIGPR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class RespawnScript : MonoBehaviour
{
    //To do:
    //!-Respawn boundaries
    //!-Checkpoints
    //!-Respawn teleportation.
    //-Add nice XML stuff
    //-Go through project standards

    //This script goes on the monkey ball and checks for collisions with the respawn layer/tag (what's more computationally efficient?).
    //triggers the respawn sequence when that happens.


    [SerializeField] GameObject blackSquare;
    [SerializeField] private float respawnTime; //Make this a define

    [SerializeField] private float fadeInTime; //Percentage of time spent fading in
    [SerializeField] private float fadeWaitTime; //Percentage of time waiting.
    [SerializeField] private float fadeOutTime;  //Percentage of time fading out.

    private BallController ballscript;
    private void Start()
    {
        ballScript = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG).GetComponent<BallController>();

    }
    IEnumerator fadeOut()
    {
        Color colour = Color.black;
        float fadeAmount = 0.0f;
        while (fadeAmount <= 1.0f)
        {
            //reduce fade amount
            fadeAmount += Time.deltaTime / (respawnTime * fadeOutTime);
            colour = new Color(colour.r, colour.g, colour.b, fadeAmount);            
            //set that colour
            blackSquare.GetComponent<RawImage>().color = colour;
            //wait for the frame to end
            yield return null;            
        }
    }

    IEnumerator fadeIn()
    {
        Color colour = Color.black;
        float fadeAmount = 1.0f;
        while (fadeAmount >=  0.0f)
        {
            //reduce fade amount
            fadeAmount -= Time.deltaTime / (respawnTime * fadeInTime);
            colour = new Color(colour.r, colour.g, colour.b, fadeAmount);
            //set that colour
            blackSquare.GetComponent<RawImage>().color = colour;
            //wait for the frame to end
            yield return null;
        }

    }

    GameObject findCheckpoint()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        GameObject closestCheckpoint = null;
        float distanceSqrToPoint = 100000.0f; //High value that will be beat by the first checkpoint.
        foreach (GameObject checkpoint in checkpoints)
        {
            float distanceSqr = Vector3.SqrMagnitude(checkpoint.transform.position - transform.position);
            if (distanceSqr < distanceSqrToPoint)
            {
                closestCheckpoint = checkpoint;
                distanceSqrToPoint = distanceSqr;
            }
        }
        return closestCheckpoint;
    }

    IEnumerator respawn()
    {
        //DisableMovement (placeholder until the PR from the tube system is merged as that includes a ready made function.)
        ballscript.DisableInput()

        //https://turbofuture.com/graphic-design-video/How-to-Fade-to-Black-in-Unity (for bibliography)
        StartCoroutine(fadeOut());
        yield return new WaitForSeconds(respawnTime * (fadeOutTime + fadeWaitTime / 2 ));
        transform.position = findCheckpoint().transform.position;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(respawnTime * (fadeWaitTime / 2) );
        StartCoroutine(fadeIn());
        yield return new WaitForSeconds(respawnTime * fadeInTime);
        blackSquare.GetComponent<RawImage>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        //reenable movement.
        ballscript.EnableInput()

    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Island Boundary") //If the shape is uneven, use a mesh collider.
        {
            StartCoroutine(respawn());
        }
    }

}
