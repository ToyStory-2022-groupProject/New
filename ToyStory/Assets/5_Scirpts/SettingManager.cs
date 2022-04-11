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
    [SerializeField] Slider[] sliders;
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
        var obj = FindObjectOfType<GameManager>();
        sliders[0].value = obj.brightnessValue;
        sliders[1].value = obj.bgmValue;
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
            case Options.Brightness:
                
                obj.brightnessValue = value;
                break;
            case Options.BGM:
                obj.mixer.SetFloat("BGM", Mathf.Log10(value) * 20);
                obj.bgmValue = value;
                break;
            case Options.Effect:
                break;
        }
    }
}
