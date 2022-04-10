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
    
    // 밝기 관련
    public Light lights;
    
    // 소리관련
    public AudioClip clip;
    public AudioSource audioSource;
    public AudioMixer mixer;
    
    void Awake()
    {
        lights = GetComponent<Light>();
        var obj = FindObjectsOfType<GameManager>();
        if(obj.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

    void Update()
    {
        Pause();
        Menu();
        lights.intensity = brightnessValue;
    }
    
    void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SubUI.Instance.LoadSubMenu();
        }
    }
    
    void Pause() // 게임 정지
    {
        var obj = FindObjectOfType<SubUI>();
        Time.timeScale = (obj == null) ? 1 : 0;
    }
}
