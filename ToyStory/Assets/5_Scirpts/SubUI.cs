using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        if (Instance != this || SceneManager.GetActiveScene().name == "MainUI")
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

    public void Resume() // 게임 재개하기
    {
        Destroy(gameObject);
    }

    public void Setting() // 세팅창 켜기
    {
        gameObject.SetActive(false);
        SettingManager.Instance.OpenSetting();
    }
    public void ReturnMenu() // 서브메뉴에서 메인으로 돌아가기
    {
        PlayerPrefs.SetInt("Save", 1);
        LoadingSceneController.Instance.LoadScene(0);
    }

    public void ExitGame() // 서브메뉴에서 게임 종료
    {
        PlayerPrefs.SetInt("Save", 0);
        LoadingSceneController.Instance.LoadScene(0);

    }
    

   void Update()
   {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
           Destroy(gameObject);
       }
   }
}
