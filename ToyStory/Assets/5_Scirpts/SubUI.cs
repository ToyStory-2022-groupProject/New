using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SubUI : MonoBehaviour
{
    static SubUI instance;
    public static SubUI Instance
    {
        get
        {
            if (instance == null)
            {
                var sub = FindObjectOfType<SubUI>();
                if (sub != null)
                {
                    instance = sub;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }
    }
    
    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    static SubUI Create()
    {
        var Sub = Resources.Load<SubUI>("SubMenu");
        return Instantiate(Sub);
    }
    
    public void LoadSubMenu()
    {
        gameObject.SetActive(true);
    }

    public void Resume()
    {
        Destroy(gameObject);
    }

    public void Setting()
    {
        gameObject.SetActive(false);
        SettingManager.Instance.OpenSetting();
    }
    public void ReturnMenu()
    {
        PlayerPrefs.SetInt("Save", 1);
        LoadingSceneController.Instance.LoadScene("MainUI");
    }

    public void ExitGame()
    {
        PlayerPrefs.SetInt("Save", 0);
        LoadingSceneController.Instance.LoadScene("MainUI");

    }
    

   void Update()
   {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
           Destroy(gameObject);
       }
   }
}
