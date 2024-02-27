using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_UI : MonoBehaviour
{
    [SerializeField] private bool isPaused = false;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private Canvas interiorUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            FadeIn();
            
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            FadeOut();
        }
    }

    public void FadeIn()
    {
        fadeAnimator.Play("fadeIn");
        isPaused = true;
        StartCoroutine(ShowPauseMenu());

    }

    public void FadeOut()
    {
        interiorUI.transform.gameObject.SetActive(false);
        fadeAnimator.Play("fadeOut");
        isPaused = false;
    }

    private IEnumerator ShowPauseMenu()
    {
        yield return new WaitForSeconds(3);
        interiorUI.transform.gameObject.SetActive(true);
    }    
}
