using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopView : MonoBehaviour
{
    public CameraManager cam;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("인 콜라이더");
            cam.TopViewIn();
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("아웃 콜라이더");
            cam.TopViewOut();
        }
    }
    
    
}
