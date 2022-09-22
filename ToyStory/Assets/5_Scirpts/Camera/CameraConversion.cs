using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConversion : MonoBehaviour
{
    [SerializeField] private GameObject[] offGameObjects;
    [SerializeField] private GameObject[] onGameObjects;

    public bool isCinematic;
    public bool isCinematicPlayed;
    
    // 만약 선풍기로 인해 시네마틱이 재생시 실행해야할 함수 일반 캠에서 시네마틱 캠으로 전환
    public void PlayCinematicCam() 
    {
        if (!isCinematicPlayed)
        {
            for (int i = 0; i < offGameObjects.Length; i++)
            {
                offGameObjects[i].SetActive(false);
            }
            for (int i = 0; i < onGameObjects.Length; i++)
            {
                onGameObjects[i].SetActive(true);
            }
        }
    }

    // 시네마틱이 끝나고 시네마틱 캠에서 일반캠으로 전환
    public void PlayNormalCam()
    {
        for (int i = 0; i < offGameObjects.Length; i++)
        {
            offGameObjects[i].SetActive(true);
        }
        for (int i = 0; i < onGameObjects.Length; i++)
        {
            onGameObjects[i].SetActive(false);
        }
        isCinematicPlayed = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !isCinematic && !isCinematicPlayed)
        {
            for (int i = 0; i < offGameObjects.Length; i++)
            {
                offGameObjects[i].SetActive(false);
            }

            for (int i = 0; i < onGameObjects.Length; i++)
            {
                onGameObjects[i].SetActive(true);
            }
        }
    }
}
