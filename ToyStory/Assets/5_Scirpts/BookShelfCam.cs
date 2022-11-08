using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class BookShelfCam : MonoBehaviour
{
    public GameObject basicCam; // 기본카메라
    public GameObject bookShelfCam; // 책장 카메라
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && PlayerController.isGrab) // 트리거 들어가고 잡기키 눌렀을때 책장 카메라로 전환
        {
            basicCam.SetActive(false);
            bookShelfCam.SetActive(true);
        }
        else
        {
            basicCam.SetActive(true);
            bookShelfCam.SetActive(false);
        }
    }
}
