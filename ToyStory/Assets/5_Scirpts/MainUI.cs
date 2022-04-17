using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainUI : MonoBehaviour
{
    [SerializeField] Button resume;
    [SerializeField] Button chapterPick;

    void Update()
    {
        Time.timeScale = 1; // 지워도 될 듯
        SaveCheck();
    }

    void SaveCheck()
    {
        int isSave = PlayerPrefs.GetInt("Save");
        if (isSave > 0)
        {
            resume.gameObject.SetActive(true);
            chapterPick.gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        int nextScene = GameManager.scene.buildIndex + 1;
        
        LoadingSceneController.Instance.LoadScene(nextScene);
        PlayerPrefs.SetInt("Save", 0);
    }
    
    public void Setting()
    {
        SettingManager.Instance.OpenSetting();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
