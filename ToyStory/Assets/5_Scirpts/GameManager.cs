using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Scene scene;
    // 소리 관련
    [SerializeField] GameObject pressF1;
    [SerializeField] AudioClip[] clips;
    public AudioMixer mixer;
    [SerializeField] AudioMixerGroup audioMixerGroup;
    AudioSource audioSource;
    int nowSceneNum;
    public int StartPoint = -1;
    // 밝기 관련
    public Light lights;

    public static bool isKeyGuide;
    public  static bool isF1;
    void Awake()
    {
        scene = SceneManager.GetActiveScene();
        audioSource = gameObject.AddComponent<AudioSource>();
        if (PlayerPrefs.GetInt("init") == 0)
        { // 게임을 실행한 적 없이 진짜 처음 시작하는 경우
            PlayerPrefs.SetFloat("BGM", 1);
            PlayerPrefs.SetFloat("Bright", 1);
            PlayerPrefs.SetInt("init", 1);
        }
        lights = GetComponent<Light>();
        var obj = FindObjectsOfType<GameManager>();
        if(obj.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

    void Start()
    {
        audioSource.loop = true;
        audioSource.outputAudioMixerGroup = audioMixerGroup;
        mixer.SetFloat("BGM", Mathf.Log10(PlayerPrefs.GetFloat("BGM")) * 20);
        lights.intensity = PlayerPrefs.GetFloat("Bright");
        PlayBGM(0);
    }

    void Update()
    {
        //Debug.Log(isF1);
        //Debug.Log(scene.buildIndex);
        if (scene.buildIndex != 0 && !isF1)
        {
            //Debug.Log("여기 들어옴");
            pressF1.SetActive(true);
        }
        else
            pressF1.SetActive(false);
        
        Pause();
        Menu();
        Keyguide();
        scene = SceneManager.GetActiveScene();
        lights.intensity = PlayerPrefs.GetFloat("Bright");
    }
    
    void Menu() // 서브메뉴창 켜기
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isKeyGuide == false)
        {
            SubUI.Instance.LoadSubMenu();
        }
    }

    void Keyguide() // 키 가이드 열기
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isF1 = true;
            isKeyGuide = true;
            KeyGuide.Instance.LoadSubMenu();
        }
    }
    
    void Pause() // 게임 정지
    {
        var obj = FindObjectOfType<SubUI>();
        Time.timeScale = (obj == null) ? 1 : 0;
    }
    
    public void PlayBGM(int num)
    {
        audioSource.clip = clips[num];
        audioSource.Play();
    }
}
