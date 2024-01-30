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


    [SerializeField] Vector3 spawnPosition;
    [SerializeField] GameObject blackSquare;
    [SerializeField] private float respawnTime; //Make this a define
    

    IEnumerator fadeOut()
    {
        Color colour = Color.black;
        float fadeAmount = 0.0f;
        while (fadeAmount <= 1.0f)
        {
            UnityEngine.Debug.Log(fadeAmount);
            //reduce fade amount
            fadeAmount += Time.deltaTime / (respawnTime * 0.4f);
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
            UnityEngine.Debug.Log(fadeAmount);
            //reduce fade amount
            fadeAmount -= Time.deltaTime / (respawnTime * 0.4f);
            colour = new Color(colour.r, colour.g, colour.b, fadeAmount);
            //set that colour
            blackSquare.GetComponent<RawImage>().color = colour;
            //wait for the frame to end
            yield return null;
        }

    }

    IEnumerator respawn()
    {
        //DisableMovement (placeholder until the PR from the tube system is merged as that includes a ready made function.)

        //https://turbofuture.com/graphic-design-video/How-to-Fade-to-Black-in-Unity (for bibliography)
        StartCoroutine(fadeOut());
        yield return new WaitForSeconds(respawnTime * 0.55f);
        transform.position = spawnPosition;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(respawnTime * 0.05f);
        StartCoroutine(fadeIn());
        yield return new WaitForSeconds(respawnTime * 0.4f);
        blackSquare.GetComponent<RawImage>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        //reenable movement.
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(0, 4, 0);
//        StartCoroutine(respawn());
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Island Boundary") //If the shape is uneven, use a mesh collider.
        {
            StartCoroutine(respawn());
        }
    }

}
