using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SFXMgr : MonoBehaviour
{
    private static SFXMgr instance;
    [SerializeField] List<AudioClip> sfxs = new List<AudioClip>();
    public AudioSource SFX; //오디오 소스 받아오기
    [SerializeField] AudioMixerGroup audioMixerGroup;
    
    public bool isPlaying;
    public enum SFXName
    {
        Walk, Run, Jump //sfx 종류
    }

    public static SFXMgr Instance 
    {
        get
        {
            return instance;
        }
        set
        {
            Instance = value;
        }
    }

    private void Awake() 
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        
        instance = this;
        SFX = GetComponent<AudioSource>(); //오디오 소스를 sfx 로 받아옴
        
        SFX.outputAudioMixerGroup = audioMixerGroup; // 효과음 조절 관련


        DontDestroyOnLoad(gameObject);
    }

    public void Play_SFX(SFXName sfxName)  //sfx이름에 알맞는 sfx 재생
    {
        
        SFX.clip = sfxs[(int)sfxName];

        if(!SFX.isPlaying)
            SFX.Play();  
    }

    public void OverlapPlay_SFX(SFXName sfxName) //중첩되게 재생 가능
    {
        SFX.PlayOneShot(sfxs[(int)sfxName]);
    }
    public void Stop_SFX()
    {
        SFX.Stop();
    }
}
