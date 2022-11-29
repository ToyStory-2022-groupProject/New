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

    public GameObject NoiseUI;
    public static bool inStage;
    private bool isTimer;
    private bool isGameOver;
    public float gameOverTime = 10f;
    private float curTime;

    public BoxCollider checkPointer6;

    private void Start()
    {
        DataManager.Checking();
        if(DataManager.dataExist)
            DataManager.Load();
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
                NoiseUI.SetActive(false);
                cat.isfound = true;
                panel.SetActive(false);
                puzzleUI.SetActive(false);
                audioSource.clip = audioClips[1];
                audioSource.Play();
                audioSource.loop = false;
            } 
        }
        if (BatteryCatch.isStop)
        {
            audioSource.Stop();
            gameObject.SetActive(false);
            checkPointer6.enabled = true;
            ClockTrigger.isClockPuzzle = false;
        }
    }

    public void Init()
    {
        curTime = 0.0f;
        audioSource.clip = audioClips[0];
        audioSource.Play();
        audioSource.loop = true;
        isGameOver = false;
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
