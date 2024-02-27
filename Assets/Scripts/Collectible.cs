using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Reference to the total collectibles count
    public CollectibleManager collectibleManager;

    private void Start()
    {
        collectibleManager = GameObject.Find("CollectibleManager").GetComponent<CollectibleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Increment the total collectibles count
            collectibleManager.IncrementCollectibles();

            // Remove the collected object from the scene
            Destroy(gameObject);
        }
    }
}

