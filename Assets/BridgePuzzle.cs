using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePuzzle : MonoBehaviour
{
    [SerializeField] private GameObject[] bridgeSwitches;
    [SerializeField] private GameObject[] bridgeSections;
    [SerializeField] private Animator[] animators;

    public bool section1Up, section2Up, section3Up;


}
