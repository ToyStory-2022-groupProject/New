using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public float bgmValue;
    public float effectValue;
    public float brightnessValue;

    public AudioClip clip;
    public AudioSource audioSource;
    public AudioMixer mixer;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

    }
}
