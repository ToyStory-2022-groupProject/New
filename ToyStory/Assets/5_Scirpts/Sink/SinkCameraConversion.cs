using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkCameraConversion : MonoBehaviour
{
    [SerializeField] private GameObject barCamGameObject;
    [SerializeField] private GameObject sinkCam;
    private GameObject tempObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            barCamGameObject.SetActive(false);
            sinkCam.SetActive(true);
            tempObject = barCamGameObject;
            barCamGameObject = sinkCam;
            sinkCam = tempObject;
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     barCamGameObject.SetActive(true);
    //     sinkCam.SetActive(false);
    // }
}
