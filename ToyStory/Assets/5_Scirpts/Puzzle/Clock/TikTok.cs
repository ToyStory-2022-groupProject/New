using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TikTok : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    
    private bool inStage;
    private bool isTimer;
    private bool isGameOver;
    public float gameOverTime = 10f;
    private float curTime;

    private void Start()
    {
        audioSource.clip = audioClips[0];
    }

    private void Update()
    {
        if(inStage)
        {
            curTime += Time.deltaTime;

            if (curTime >= gameOverTime && isGameOver == false)
            {
                isGameOver = true;
                Debug.Log("게임오버!!!"); // 마녀가 죽이는거 추가하기
                audioSource.clip = audioClips[1];
                audioSource.Play();
                audioSource.loop = false;
            } 
        }

        if (BatteryCatch.isStop)
        {
            audioSource.Stop();
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inStage = true;
            audioSource.Play();
        }
    }
}
