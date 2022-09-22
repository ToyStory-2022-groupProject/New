using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkCameraConversion : MonoBehaviour
{
    [SerializeField] private GameObject barCamGameObject;
    [SerializeField] private GameObject sinkCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            barCamGameObject.SetActive(false);
            sinkCam.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        barCamGameObject.SetActive(true);
        sinkCam.SetActive(false);
    }
}
