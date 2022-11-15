using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    private GameManager _gameManager;
    public bool isFade;
    private float startBgmVolume;
    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
        startBgmVolume = _gameManager.audioSource.volume;
    }

    private void Update()
    {
        if (isFade)
        {
            Debug.Log("실행중");
            isFade = false;
            StartCoroutine("BgmFadeOut");
        }
    }

    public IEnumerator BgmFadeOut()
    {
        while (_gameManager.audioSource.volume >= 0f)
        {
            Debug.Log("소리나오는중");
            _gameManager.audioSource.volume -= startBgmVolume / _gameManager.fadeTime;
            yield return null;
        }
    }
}
