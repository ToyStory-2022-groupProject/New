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

    public DataManager dataManager;
    private bool saveExist;
    void Start()
    {
        dataManager.Checking();
        SaveCheck();
    }
    void Update()
    {
        Time.timeScale = 1; // 지워도 될 듯
    }

    void SaveCheck()
    {
        if (dataManager.dataExist)
        {
            saveExist = true;
            resume.gameObject.SetActive(true);
            chapterPick.gameObject.SetActive(true);
        }
        else if(!dataManager.dataExist) 
            saveExist = false;
    }

    int nextScene;
    public void StartGame()
    {
        if(!saveExist)
        {
            nextScene = GameManager.scene.buildIndex + 1;
        }     
        else if(saveExist)
        {
            dataManager.Load();
            nextScene = dataManager.StageNum;
            Debug.Log(dataManager.StageNum);
            Debug.Log(dataManager.PointNum);
        }
            
        LoadingSceneController.Instance.LoadScene(nextScene);
        //PlayerPrefs.SetInt("Save", 0);
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