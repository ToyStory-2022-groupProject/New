using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockTrigger : MonoBehaviour
{
    public static bool isClockPuzzle;
    public GameObject ClockUI;
    public GameObject panel;
    public GameObject NoiseUI;

    private void OnTriggerStay(Collider other)
    {
        if (PlayerController.isGrab && other.gameObject.layer == 3)
        {
            ClockUI.SetActive(true);
            panel.SetActive(true);
            isClockPuzzle = true;
            NoiseUI.SetActive(false);
        }
    }
}
