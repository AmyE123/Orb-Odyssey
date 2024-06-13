using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
using static CT6RIGPR.Constants;

public class LevelSelector : MonoBehaviour
{
    //Image for each area to highlight/unlight
    private enum Level {RollingIsles, LeapLagoon, BoingBay, GrassyKnoll }
    private Level _level;

    [SerializeField] private Image _levelBackground1;
    [SerializeField] private Image _levelBackground2;
    [SerializeField] private Image _levelBackground3;
    [SerializeField] private Image _levelBackground4;

    [SerializeField] private Color _highlightColour;

    private void UpdateHighlight()
    {
        switch (_level)
        {
            case Level.RollingIsles:
                _levelBackground4.color = Color.black;
                _levelBackground1.color = _highlightColour;
                break;
            case Level.LeapLagoon:
                _levelBackground1.color = Color.black;
                _levelBackground2.color = _highlightColour;
                break;
            case Level.BoingBay:
                _levelBackground2.color = Color.black;
                _levelBackground3.color = _highlightColour;
                break;
            case Level.GrassyKnoll:
                _levelBackground3.color = Color.black;
                _levelBackground4.color = _highlightColour;
                break;
        }
    }

    private void CycleSelectedLevel()
    {
        if (_level == Level.GrassyKnoll)
        {
            _level = 0;
        }
        else
        {
            _level++;
        }
        UpdateHighlight();
    }

    private void CheckForCycle()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            CycleSelectedLevel();
        }

        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Placeholder for activation");

            if (_level == Level.RollingIsles)
            {
                SceneManager.LoadScene(1);
            }
            else if (_level == Level.LeapLagoon)
            {
                SceneManager.LoadScene(2);
            }
            else if (_level == Level.BoingBay)
            {
                SceneManager.LoadScene(3);
            }
            else if (_level == Level.GrassyKnoll)
            {
                SceneManager.LoadScene(4);
            }
            else
            {
                Debug.Log("Couldn't select a level");
            }
        }
    }

    private void Update()
    {
        CheckForCycle();
    }
}