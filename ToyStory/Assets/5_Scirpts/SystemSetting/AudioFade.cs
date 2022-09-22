using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFade : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
    }

    private void Update()
    {
        StartCoroutine("BgmFadeOut");
    }

    public IEnumerator BgmFadeOut()
    {
        float startBgmVolume = _gameManager.audioSource.volume;

        while (_gameManager.audioSource.volume > 0)
        {
            _gameManager.audioSource.volume -= startBgmVolume * Time.deltaTime / _gameManager.fadeTime;
            yield return null;
        }
        
        _gameManager.audioSource.Stop();
        _gameManager.audioSource.volume = startBgmVolume;
    }
}
