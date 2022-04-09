using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainUI : MonoBehaviour
{
    [SerializeField] Button resume;
    [SerializeField] Button chapterPick;
    
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1;
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
        LoadingSceneController.Instance.LoadScene("InGame");
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
