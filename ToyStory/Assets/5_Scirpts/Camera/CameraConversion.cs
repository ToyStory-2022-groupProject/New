using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConversion : MonoBehaviour
{
    [SerializeField] Vector3 posOffset;
    [SerializeField] Vector3 rotOffset;
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("인 콜라이더");
            CameraManager.offset = posOffset;
            CameraManager.isRotate = true;
            CameraManager.targetRot = rotOffset;
            //Conversion();
        }
    }
    
}
