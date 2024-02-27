using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    public int totalCollectibles = 0;
    private Text collectibleText;


    private void Start()
    {
        collectibleText = GameObject.Find("CollectibleCounterText").GetComponent<Text>();
        Debug.Log("Found the text");
        UpdateText();
    }

    // Method to increment the total collectibles count
    public void IncrementCollectibles()
    {
        totalCollectibles++;
        // You can add additional actions here if needed when a collectible is picked up
        Debug.Log("Total collectibles: " + totalCollectibles);
        UpdateText();
    }

    private void UpdateText()
    {
        if (collectibleText != null)
        {
            collectibleText.text = "Collectibles: " + totalCollectibles;
        }
    }
}
