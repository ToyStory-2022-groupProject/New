using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Fan_Audio : MonoBehaviour
{
    [Header("오디오")] 
    [SerializeField] AudioClip[] audioclips;
    AudioSource audioSource;
    bool isSwitchOn;
    int cnt;
    bool isSwitchPlay;
    float timer;
    void Awake()
    {
        timer = 2f;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Debug.Log("isswi" + isSwitchOn);
        if(isSwitchOn) 
            OnMusic();
        else
        {
            audioSource.Pause();
        }
    }

    void OnMusic()
    {
        timer -= Time.deltaTime;
        Debug.Log("timer" + timer);
        if (timer < 2f && cnt == 0)
        {
            Debug.Log("0재생");
            audioSource.clip = audioclips[0];
            audioSource.Play();
            cnt++;
        }
        else if (timer < 1.5f && cnt == 1)
        {
            Debug.Log("1재생");
            audioSource.clip = audioclips[1];
            audioSource.loop = true;
            audioSource.Play();
            cnt++;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 3 && PlayerController.isGrab)
        {
            isSwitchOn = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3 && !PlayerController.isGrab)
        {
            isSwitchOn = false;
            timer = 2f;
            cnt = 0;
        }
    }
}