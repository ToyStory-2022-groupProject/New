using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.IO;


public class LoadingSceneController : MonoBehaviour
{
    static LoadingSceneController instance;

    public static LoadingSceneController Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<LoadingSceneController>();
                
                if (obj != null)
                    instance = obj;
                else
                    instance = Create();
            }
            return instance;
        }
    }

    [SerializeField] Image progressBar;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Text tip;
    
    int ran;
    string[] tips;
    int loadSceneNum;
    bool isWait;
    
    static LoadingSceneController Create()
    {
        var loadPrefab = Resources.Load<LoadingSceneController>("LoadScene");
        return Instantiate(loadPrefab);
    }
    
    void Awake()
    {
        Reading();
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    
    

    public void LoadScene(int sceneNumber) // 씬 불러오는 함수
    {
        gameObject.SetActive(true);
        TipChange();
        SceneManager.sceneLoaded += LoadSceneEnd;
        loadSceneNum = sceneNumber;
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));

        AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneNum);
        async.allowSceneActivation = false;

        float timer = 0f;
        while (!async.isDone)
        {
            yield return null;

            timer += Time.unscaledTime * 0.01f; // 수정
            if (progressBar.fillAmount < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(0, async.progress, timer);
                progressBar.color = Color.Lerp(Color.red, Color.green, timer);
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1, timer * 0.1f);
                progressBar.color = Color.Lerp(progressBar.color, Color.green, timer);
                if (progressBar.fillAmount >= 0.99999f)
                {
                    async.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;

        while (timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledTime * 1f; // 이 부분 나중에 수정
            canvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);

        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }

    void LoadSceneEnd(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == loadSceneNum)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }
    
    void Reading()
    {
        TextAsset textFile = Resources.Load("sample") as TextAsset;
        tips = textFile.text.Split('\n');
    }
    
    void TipChange()
    {
        ran = UnityEngine.Random.Range(0, tips.Length);
        tip.text = string.Format("Tip : " + tips[ran]);
    }
}
