using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public enum Options
    {
        Brightness,
        BGM,
        Effect
    };

    public Options opType;
    static SettingManager instance;
    public Slider[] sliders;
    [SerializeField] Image image;
    
    
    static public SettingManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SettingManager>();
                if (obj != null)
                    instance = obj;
                else
                {
                    instance = Create();
                }
            }

            return instance;
        }
    }
    static SettingManager Create()
    {
        var loadPrefab = Resources.Load<SettingManager>("SettingUI");
        return Instantiate(loadPrefab);
    }


    public void OpenSetting()
    {
        gameObject.SetActive(true);
        BackGround();
        sliders[0].value = PlayerPrefs.GetFloat("Bright");
        sliders[1].value = PlayerPrefs.GetFloat("BGM");
        sliders[2].value = PlayerPrefs.GetFloat("Effort");
    }
    
    void Update()
    {
        Close();
    }

    void Close()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(gameObject);
        }
    }

    void BackGround()
    {
        if (SceneManager.GetActiveScene().name == "MainUI")
        {
            image.gameObject.SetActive(true);
        }
    }
    
    public void ValueChange(float value)
    {
        var obj = FindObjectOfType<GameManager>();
        
        switch (opType)
        {
            // 설정값 저장 및 조정
            case Options.Brightness:
                PlayerPrefs.SetFloat("Bright", value);
                break;
            case Options.BGM:
                obj.mixer.SetFloat("BGM", Mathf.Log10(value) * 20);
                PlayerPrefs.SetFloat("BGM", value);
                break;
            case Options.Effect:
                obj.mixer.SetFloat("Effort", Mathf.Log10(value) * 20);
                PlayerPrefs.SetFloat("Effort", value);
                break;
        }
    }
}