using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Water;

public class Sink : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject effect;
    [SerializeField] GameObject water;
    AudioSource _audioSource;
    public float speed;
    PlayerController playerController;
    bool isSwitch;
    bool isSound;
    void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isSwitch && PlayerController.isGrab)
        {
            if (!isSound)
            {
                _audioSource.Play();
                isSound = true;
            }
            effect.SetActive(true);
            WaterUp();
        }
        else
        {
            _audioSource.Pause();
            effect.SetActive(false);
            isSound = false;
        }
    }
    
    void WaterUp()
    {
        if (water.transform.position.y < 7.8f)
        {
            
            water.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            isSwitch = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            isSwitch = false;
        }
    }
}
