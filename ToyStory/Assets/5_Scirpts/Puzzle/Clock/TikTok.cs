using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TikTok : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public Cat cat;
    public GameObject puzzleUI;
    public GameObject panel;
    public DataManager DataManager;

    private bool inStage;
    private bool isTimer;
    private bool isGameOver;
    public float gameOverTime = 10f;
    private float curTime;

    private void Start()
    {
        DataManager.Checking();
        if(DataManager.dataExist)
            DataManager.Load();
        audioSource.clip = audioClips[0];
    }

    private void Update()
    {
        if(DataManager.PointNum < 6)
        {
            if(inStage)
        {
            curTime += Time.deltaTime;

            if (curTime >= gameOverTime && isGameOver == false)
            {
                isGameOver = true;
                cat.isfound = true;
                panel.SetActive(false);
                puzzleUI.SetActive(false);
                audioSource.clip = audioClips[1];
                audioSource.Play();
                audioSource.loop = false;
                curTime = 0.0f;
            } 
        }

        if (BatteryCatch.isStop)
        {
            audioSource.Stop();
            gameObject.SetActive(false);
        }
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
