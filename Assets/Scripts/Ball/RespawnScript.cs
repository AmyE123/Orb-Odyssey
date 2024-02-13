using CT6RIGPR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

/// <summary>
/// Handles the respawn mechanic when the player moves out of the island's boundary.
/// </summary>
public class RespawnScript : MonoBehaviour
{
    [SerializeField] GameObject blackSquare;
    [SerializeField] private float fadeInTime; //Percentage of time spent fading in
    [SerializeField] private float fadeWaitTime; //Percentage of time waiting.
    [SerializeField] private float fadeOutTime;  //Percentage of time fading out.

    private BallController ballScript;

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
            fadeAmount += Time.deltaTime / (Constants.RESPAWN_TIME * fadeOutTime);
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
            fadeAmount -= Time.deltaTime / (Constants.RESPAWN_TIME * fadeInTime);
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
        float distanceSqrToPoint = 100000.0f; //High value that will be beaten by the first checkpoint.
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
        //DisableMovement
        ballScript.DisableInput();

        StartCoroutine(fadeOut());
        yield return new WaitForSeconds(Constants.RESPAWN_TIME * (fadeOutTime + fadeWaitTime / 2 ));
        transform.position = findCheckpoint().transform.position;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(Constants.RESPAWN_TIME * (fadeWaitTime / 2) );
        StartCoroutine(fadeIn());
        yield return new WaitForSeconds(Constants.RESPAWN_TIME * fadeInTime);
        blackSquare.GetComponent<RawImage>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        //Reenable movement.
        ballScript.EnableInput();

    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Island Boundary")
        {
            StartCoroutine(respawn());
        }
    }

}
